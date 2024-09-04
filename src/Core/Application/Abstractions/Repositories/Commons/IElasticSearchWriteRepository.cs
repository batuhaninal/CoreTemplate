
namespace Application.Abstractions.Repositories.Commons
{
    public interface IElasticSearchWriteRepository
    {
        Task CreateAsync<T>(string indexName, T entity);
        Task RemoveAsync<T>(string indexName, string id);
        Task UpdateAsync<T>(string indexName, string id, T entity);
    }
}
