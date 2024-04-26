namespace Application.Models.DTOs.Products
{
    public record ProductItemDto
    {
        public string ProductId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public decimal Price { get; init; }
        public string CategoryId { get; init; } = null!;
        public DateTime CreatedTime { get; init; }
    }
}
