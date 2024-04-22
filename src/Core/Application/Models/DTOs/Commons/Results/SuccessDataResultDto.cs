namespace Application.Models.DTOs.Commons.Results
{
    public class SuccessDataResultDto<T> : DataResultDto<T>
    {
        public SuccessDataResultDto(T data) : base(data, 200, true)
        {

        }

        public SuccessDataResultDto(T data, string message) : base(data, 200, true, message)
        {

        }

        public SuccessDataResultDto(T data, int statusCode, string message) : base(data, statusCode, true, message)
        {

        }

        public SuccessDataResultDto(string message) : base(default, 200, true, message)
        {

        }

        public SuccessDataResultDto(int statusCode, string message) : base(default, statusCode, true, message)
        {

        }

        public SuccessDataResultDto() : base(default, 200, true)
        {

        }
    }
}
