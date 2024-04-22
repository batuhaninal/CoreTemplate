namespace Application.Models.DTOs.Commons.Results
{
    public class SuccessResultDto : ResultDto
    {
        public SuccessResultDto(int statusCode, string message) : base(statusCode, true, message)
        {

        }

        public SuccessResultDto(string message) : base(200, true, message)
        {

        }

        public SuccessResultDto() : base(200, true)
        {

        }
    }
}
