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

        public SearchComponentViewModel(IEnumerable<SearchWriterDto> writers, IEnumerable<SearchCategoryDto> categories, IEnumerable<SearchArticleDto> articles)
        {
            Writers = writers;
            Categories = categories;
            Articles = articles;
        }

        public IEnumerable<SearchWriterDto> Writers { get; init; }
        public IEnumerable<SearchCategoryDto> Categories { get; init; }
        public IEnumerable<SearchArticleDto> Articles { get; init; }
    }
}
