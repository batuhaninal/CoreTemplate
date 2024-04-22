namespace Application.Abstractions.Commons.Results
{
    public interface IDataResult<T> : IBaseResult
    {
        T Data { get; }
    }
}
