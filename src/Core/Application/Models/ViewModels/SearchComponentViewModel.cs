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

        public SearchComponentViewModel(List<SearchWriterDto> writers, List<SearchCategoryDto> categories, List<SearchArticleDto> articles)
        {
            Writers = writers ?? new();
            Categories = categories ?? new();
            Articles = articles ?? new();
        }

        public List<SearchWriterDto> Writers { get; init; }
        public List<SearchCategoryDto> Categories { get; init; }
        public List<SearchArticleDto> Articles { get; init; }
    }
}
