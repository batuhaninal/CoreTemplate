using Application.Abstractions.Commons.Results;
using Application.Abstractions.Repositories.Users.Elasticsearch;
using Application.Models.Constants.Elastics;
using Application.Models.DTOs.Users;
using Application.Models.RequestParameters.Commons;
using Application.Utilities.Pagination;
using Elastic.Clients.Elasticsearch;
using System.Collections.Immutable;

namespace Persistence.Repositories.Users.Elasticsearch
{
    public class ELKUserRepository : IELKUserRepository
    {
        private readonly ElasticsearchClient _client;
        private const string _index = ElasticIndexes.UserIndex;
        public ELKUserRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        private async Task<SearchResponse<SecuredUserDto>> FuzzyAsync(string condition, int pageIndex, int pageSize) =>
            await _client.SearchAsync<SecuredUserDto>(s =>
            s.Index(_index)
            .From(pageIndex - 1)
            .Size(pageSize)
            .Query(q => 
                q.Bool(b => 
                    b.Should(
                        sh => sh.Fuzzy(f => f.Field(fi => fi.FirstName).Value(condition).Fuzziness(new Fuzziness(2))),
                        sh => sh.Fuzzy(f => f.Field(fi => fi.LastName).Value(condition).Fuzziness(new Fuzziness(2))),
                        sh => sh.Fuzzy(f => f.Field(fi => fi.Email).Value(condition).Fuzziness(new Fuzziness(2)))
            ))));

        public async Task<IImmutableList<SecuredUserDto>> FuzzySearchAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var response = await FuzzyAsync(condition, pageIndex, pageSize);

            if(!response.IsValidResponse)
                return ImmutableList.Create<SecuredUserDto>();

            return response.Documents.ToImmutableArray();
        }

        public async Task<IImmutableList<SecuredUserDto>> FuzzySearchAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchAsync(condition, pagination.PageIndex, pagination.PageSize);

        public async Task<IPaginatedDataResult<SecuredUserDto>> FuzzySearchWithPaginationAsync(string condition, int pageIndex = 0, int pageSize = 10)
        {
            var response = await FuzzyAsync(condition, pageIndex, pageSize);

            return response.ToPaginatedListDto(pageIndex, pageSize);
        }

        public async Task<IPaginatedDataResult<SecuredUserDto>> FuzzySearchWithPaginationAsync(string condition, BasePaginationRequestParameter pagination) =>
            await FuzzySearchWithPaginationAsync(condition, pagination.PageIndex, pagination.PageSize);
    }
}
