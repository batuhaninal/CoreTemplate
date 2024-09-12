using Application.Abstractions.Repositories.Categories.Elasticsearch;
using Application.Models.Constants.Elastics;
using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using System.Collections.Immutable;

namespace Persistence.Repositories.Categories.Elasticsearch
{
    public class ELKCategoryRepository : IELKCategoryRepository
    {
        private readonly ElasticsearchClient _client;

        private const string _index = ElasticIndexes.CategoryIndex;

        public ELKCategoryRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        private async Task<SearchResponse<Category>> FuzzyAsync(string condition, int pageIndex, int pageSize) =>
            await _client.SearchAsync<Category>(s =>
            s.Index(_index)
            .From(pageIndex - 1)
            .Size(pageSize)
            .Query(q =>
                q.Fuzzy(f =>
                    f.Field(fi =>
                        fi.Title)
                    .Value(condition)
                    .Fuzziness(new Fuzziness(2)))));

        public async Task<IImmutableList<Category>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var response = await FuzzyAsync(condition, pageIndex, pageSize);

            if(!response.IsValidResponse)
                return ImmutableList.Create<Category>();

            var data = response.Documents.ToImmutableList();

            return data;
        }

        public async Task<IImmutableList<Category>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchAsync(condition, pagination.PageIndex, pagination.PageSize);

        public async Task<PaginatedListDto<Category>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var data = await FuzzyAsync(condition, pageIndex, pageSize);

            return data.ToPaginatedListDto(pageIndex, pageSize);
        }

        public async Task<PaginatedListDto<Category>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchWithPaginationAsync(condition, pagination.PageIndex, pagination.PageSize);
    }
}
