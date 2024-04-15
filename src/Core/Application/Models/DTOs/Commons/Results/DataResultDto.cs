using Application.Abstractions.Commons.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
