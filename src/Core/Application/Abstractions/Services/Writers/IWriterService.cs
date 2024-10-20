using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Writers;
using Application.Models.RequestParameters.Commons;
using Application.Models.RequestParameters.Writers;

namespace Application.Abstractions.Services.Writers
{
    public interface IWriterService
    {
        Task<IBaseResult> CreateAsync(CreateWriterDto createWriterDto);
        Task<IBaseResult> RemoveAsync(Guid writerId);
        Task<IBaseResult> ChangeStatusAsync(Guid writerId);
        Task<IBaseResult> AddToFavAsync(Guid writerId);
        Task<IBaseResult> CreateFavAsync(Guid writerId, Guid userId);
        Task<IDataResult<WriterInfoDto>> GetByIdAsync(Guid writerId);
        Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);
        Task<IPaginatedDataResult<WriterItemDto>> GetAllAsync(WriterRequestParameter parameter);
        Task<IPaginatedDataResult<SearchWriterDto>> SearchAsync(string condition, BasePaginationRequestParameter pagination);
    }
}
