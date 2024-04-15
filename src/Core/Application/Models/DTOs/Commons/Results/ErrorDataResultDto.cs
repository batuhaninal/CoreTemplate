namespace Application.Models.DTOs.Commons.Results
{
    public class ErrorDataResultDto<T> : DataResultDto<T>
    {
        public ErrorDataResultDto() : base(default, 500, false)
        {

        }

        public ErrorDataResultDto(int statusCode) : base(default, statusCode, false)
        {

        }

        public ErrorDataResultDto(int statusCode, string message) : base(default, statusCode, false, message)
        {

        }

        public ErrorDataResultDto(string message) : base(default, 500, false, message)
        {

        }
    }
}
