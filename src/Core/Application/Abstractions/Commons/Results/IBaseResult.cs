using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Commons.Results
{
    public interface IBaseResult
    {
        int StatusCode { get; }
        bool Success { get; }
        string Message { get; }
    }
}
