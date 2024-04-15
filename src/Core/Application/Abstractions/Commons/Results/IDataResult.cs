using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Commons.Results
{
    public interface IDataResult<T> : IBaseResult
    {
        T Data { get; }
    }
}
