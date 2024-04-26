namespace Application.Models.DTOs.Products
{
    public record CreateProductDto
    {
        public string Title { get; init; } = null!;
        public decimal Price { get; init; }
        public string CategoryId { get; init; } = null!;
    }
}
