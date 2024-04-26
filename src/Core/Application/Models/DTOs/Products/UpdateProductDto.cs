namespace Application.Models.DTOs.Products
{
    public record UpdateProductDto
    {
        public string ProductId { get; init; } = null!;
        public string? Title { get; init; }
        public decimal Price { get; init; }
    }
}
