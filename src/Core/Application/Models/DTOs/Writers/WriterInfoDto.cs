using Application.Models.DTOs.Users;

namespace Application.Models.DTOs.Writers
{
    public class WriterInfoDto
    {
        public Guid WriterId { get; init; }
        public string Nick { get; init; } = null!;
        public UserInfoDto User { get; init; } = null!;
        public string Level { get; init; } = null!;
    }
}
