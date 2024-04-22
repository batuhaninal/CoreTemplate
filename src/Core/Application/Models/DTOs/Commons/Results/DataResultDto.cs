using Application.Abstractions.Commons.Results;

namespace Application.Models.DTOs.Commons.Results
{
    public class DataResultDto<T> : ResultDto, IDataResult<T>
    {
        public DataResultDto(T data, int statusCode, bool success, string message) : base(statusCode, success, message)
        {
            Data = data;
        }
        public DataResultDto(T data, int statusCode, bool success) : base(statusCode, success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
