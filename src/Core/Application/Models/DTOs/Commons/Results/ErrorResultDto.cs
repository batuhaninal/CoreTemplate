using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Commons.Results
{
    public class ErrorResultDto : ResultDto
    {
        public ErrorResultDto() : base(500, false)
        {

        }

        public ErrorResultDto(string message) : base(500, false, message)
        {

        }

        public ErrorResultDto(int statusCode) : base(statusCode, false)
        {

        }

        public ErrorResultDto(int statusCode, string message) : base(statusCode, false, message)
        {

        }
    }
}
