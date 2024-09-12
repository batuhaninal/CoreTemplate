using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Writers.Elasticsearch;
using Application.Models.Constants.Elastics;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using System.Collections.Immutable;

namespace Persistence.Repositories.Writers.Elasticsearch
{
    public class ELKWriterRepository : IELKWriterRepository
    {
        private const string _index = ElasticIndexes.WriterIndex;
        private readonly ElasticsearchClient _client;

        public ELKWriterRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<SearchResponse<Writer>> FuzzyAsync(string condition, int pageIndex, int pageSize) =>
            await _client.SearchAsync<Writer>(s =>
            s.Index(_index)
            .From(pageIndex - 1)
            .Size(pageSize)
            .Query(q =>
                q.Fuzzy(f =>
                    f.Field(fi =>
                        fi.Nick)
                    .Value(condition)
                    .Fuzziness(new Fuzziness(2)))));

        public async Task<IImmutableList<Writer>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var response = await FuzzyAsync(condition, pageIndex, pageSize);

            if(response.IsValidResponse)
                return ImmutableList.Create<Writer>();

            return response.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<Writer>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchAsync(condition, pagination.PageIndex, pagination.PageSize);

        public async Task<IPaginatedDataResult<Writer>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var data = await FuzzyAsync(condition, pageIndex, pageSize);

            return data.ToPaginatedListDto(pageIndex, pageSize);
        }

        public async Task<IPaginatedDataResult<Writer>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchWithPaginationAsync(condition, pagination.PageIndex, pagination.PageSize);
    }
}
