﻿using Application.Abstractions.Commons.Results;
using Application.Models.DTOs.Articles;
using Application.Models.RequestParameters.Commons;

namespace Application.Abstractions.Services.Articles
{
    public interface IArticleService
    {
        Task<IBaseResult> CreateAsync(CreateArticleDto createArticleDto);
        Task<IBaseResult> UpdateAsync(string articleId, UpdateArticleDto updateArticleDto);
        Task<IBaseResult> RemoveAsync(string articleId);
        Task<IDataResult<ArticleInfoDto>> GetByIdAsync(string articleId);
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(int pageIndex = 1, int pageSize = 20);
        Task<IPaginatedDataResult<ArticleItemDto>> GetAllAsync(BasePaginationRequestParameter pagination);

    }
}