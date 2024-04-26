﻿using Application.Models.DTOs.Categories;

namespace Application.Models.DTOs.Products
{
    public record ProductInfoDto
    {
        public string ProductId { get; init; } = null!;
        public string Title { get; init; } = null!;
        public decimal Price { get; init; }
        public CategoryItemDto Category { get; init; } = null!;
        public DateTime CreatedTime { get; init; }
    }
}