using Microsoft.AspNetCore.Http;

namespace Application.Models.DTOs.Commons.Files
{
    public record CreateFileDto
    {
        public string Path { get; init; } = null!;
        public IFormFileCollection FormFiles { get; init; } = null!;
    }
}