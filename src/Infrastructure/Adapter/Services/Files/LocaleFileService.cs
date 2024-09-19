using Application.Abstractions.Commons.Files;
using Application.Abstractions.Helpers;
using Application.Models.Constants.Settings;
using Application.Models.DTOs.Commons.Files;
using Application.Models.DTOs.Commons.Results;
using Application.Models.Enums;
using Application.Utilities.Exceptions.Commons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Adapter.Services.Files
{
    public class LocaleFileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _baseFolder;
        private readonly ILogger<LocaleFileService> _logger;

        public LocaleFileService(IWebHostEnvironment webHostEnvironment, ILogger<LocaleFileService> logger)
        {
            _env = webHostEnvironment;
            _logger = logger;
            _baseFolder = _env.WebRootPath;
        }
        public void Remove(string path)
        {
            try
            {
                if(!string.IsNullOrEmpty(path))
                {
                    File.Delete(Path.Combine(_baseFolder, path));
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.LogError("DirectoryNotFound Ex Message: " + ex.Message);
            }
            catch (IOException ex)
            {
                _logger.LogError("IOException Message: " + ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unexpected Exception Message: " + ex.Message);
            }
        }

        public async Task<List<FileResponseDto>> UploadAsync(CreateFileDto createFileDto, FileEnums fileEnums = FileEnums.IMAGE)
        {
            string uplodatPath = Path.Combine(_baseFolder, createFileDto.Path);
            
            if(!Directory.Exists(uplodatPath))
                Directory.CreateDirectory(uplodatPath);
            
            var list = new List<FileResponseDto>();

            foreach (FormFile file in createFileDto.FormFiles)
            {
                DateTime currentTime = DateTime.Now;
                string ext = Path.GetExtension(file.FileName);
                CheckExtension(ext, fileEnums);
                string newFileName = TextHelpers.CreateUniqueText() + ext;
                var fileResult = await CopyFileAsync(Path.Combine(uplodatPath, newFileName), file);
                if(fileResult)
                    list.Add(new FileResponseDto(newFileName, ext, Path.Combine(createFileDto.Path, newFileName), Path.Combine(_baseFolder, createFileDto.Path, newFileName), file.Length, 201, true));
                else
                    list.Add(new FileResponseDto("","","","",0,400, false, "Unexpected file IO exception"));
            }

            return list;
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile formFile)
        {
            try
            {
                using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError("IOException Message: " + ex.Message);
                return false;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Unexpected Exception Message: " + ex.Message);
                return false;
            }
        }

        private void CheckExtension(string ext, FileEnums fileEnums)
        {
            if(string.IsNullOrEmpty(ext))
                throw new ArgumentNullException(nameof(ext));
            
            ext = ext.ToLower();

            switch (fileEnums)
            {
                case FileEnums.IMAGE:
                    if(!SettingConstant.AllowedImages.Contains(ext))
                        throw new BusinessException($"{ext} extension is not allowed!");
                break;
                default:
                    throw new BusinessException($"{ext} extension is not allowed!");
            }
        }
    }
}