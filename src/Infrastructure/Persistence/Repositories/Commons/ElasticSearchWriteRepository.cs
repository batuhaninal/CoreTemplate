using Application.Abstractions.Repositories.Commons;
using Application.Utilities.Exceptions.Commons;
using Domain.Entities.Commons;
using Elastic.Clients.Elasticsearch;

namespace Persistence.Repositories.Commons
{
    public class ElasticSearchWriteRepository : IElasticSearchWriteRepository
    {
        private readonly ElasticsearchClient _client;

        public ElasticSearchWriteRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task CreateAsync<T>(string indexName, T entity)
        {
            var response = await _client.IndexAsync(entity, x => x.Index(indexName));

            if (!response.IsSuccess())
                throw new BusinessException("Elastic search create error");
        }

        public async Task DeleteAsync(string indexName, string id)
        {
            var response = await _client.DeleteAsync(id, x=> x.Index(indexName));

            if (!response.IsSuccess())
                throw new BusinessException("Elastic search update error");
        }

        public async Task UpdateAsync<T>(string indexName, string id, T entity)
        {
            var response = await _client.UpdateAsync<T, T>(index: indexName, id: id, x=> x.Doc(entity));

            if (!response.IsSuccess())
                throw new BusinessException("Elastic search update error");
        }
    }
}
