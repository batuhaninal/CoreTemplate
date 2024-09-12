using Application.Models.DTOs.Articles;
using Application.Models.DTOs.Categories;
using Application.Models.DTOs.Writers;

namespace Application.Models.ViewModels
{
    public record SearchComponentViewModel
    {
        public SearchComponentViewModel()
        {
            
        }

        public SearchComponentViewModel(IList<SearchWriterDto> writers, IList<SearchCategoryDto> categories, IList<SearchArticleDto> articles)
        {
            Writers = writers;
            Categories = categories;
            Articles = articles;
        }

        public IList<SearchWriterDto> Writers { get; init; }
        public IList<SearchCategoryDto> Categories { get; init; }
        public IList<SearchArticleDto> Articles { get; init; }
    }
}
