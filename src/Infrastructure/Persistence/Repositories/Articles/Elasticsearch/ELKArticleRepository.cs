using Application.Abstractions.Repositories.Articles.Elasticsearch;
using Application.Models.Constants.Elastics;
using Application.Models.DTOs.Commons.Results;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using System.Collections.Immutable;

namespace Persistence.Repositories.Articles.Elasticsearch
{
    public class ELKArticleRepository : IELKArticleRepository
    {
        private readonly ElasticsearchClient _client;
        private const string _index = ElasticIndexes.ArticleIndex;

        public ELKArticleRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        private async Task<SearchResponse<Article>> FuzzyAsync(string condition, int pageIndex, int pageSize) =>
            await _client.SearchAsync<Article>(s =>
            s.Index(_index)
            .From(pageIndex - 1)
            .Size(pageSize)
            .Query(q =>
                q.Fuzzy(f =>
                    f.Field(fi =>
                        fi.Title)
                    .Value(condition)
                    .Fuzziness(new Fuzziness(2)))));

        public async Task<IImmutableList<Article>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var response = await FuzzyAsync(condition, pageIndex, pageSize);

            if(!response.IsValidResponse)
                return ImmutableList.Create<Article>();

            return response.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<Article>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchAsync(condition, pagination.PageIndex, pagination.PageSize);

        public async Task<PaginatedListDto<Article>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var data = await FuzzyAsync(condition, pageIndex, pageSize);

            return data.ToPaginatedListDto(pageIndex, pageSize);
        }

        public async Task<PaginatedListDto<Article>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchWithPaginationAsync(condition, pagination.PageIndex, pagination.PageSize);
    }
}
