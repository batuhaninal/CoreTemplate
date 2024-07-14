using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.RequestParameters.Commons;

namespace Application.Abstractions.Services.Writers
{
    public interface IWriterService
    {
        Task<IBaseResult> CreateAsync(CreateWriterDto createWriterDto);
        Task<IBaseResult> RemoveAsync(string writerId);
        Task<IDataResult<WriterInfoDto>> GetByIdAsync(string writerId);
        Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);
    }
}
