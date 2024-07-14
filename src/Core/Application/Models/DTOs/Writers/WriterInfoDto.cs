using Application.Models.DTOs.Users;
using Application.Models.Enums;

namespace Application.Models.DTOs.Writers
{
    public class WriterInfoDto
    {
        public string WriterId { get; init; } = null!;
        public string Nick { get; init; } = null!;
        public UserInfoDto User { get; init; } = null!;
        public string Level { get; init; } = null!;
    }
}
