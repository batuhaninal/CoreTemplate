using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Writers;

namespace Application.Abstractions.Services.Writers
{
    public interface IWriterService
    {
        Task<IBaseResult> CreateAsync(CreateWriterDto createWriterDto);
        Task<IBaseResult> RemoveAsync(string writerId);
        Task<IDataResult<WriterInfoDto>> GetByIdAsync(string writerId);
        Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(WriterRequestParameter parameter, BasePaginationRequestParameter pagination);
        Task<IDataResult<IList<SearchWriterDto>>> SearchAsync(string condition, int size = 10);
    }
}
