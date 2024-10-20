namespace Application.Models.DTOs.Writers
{
    public class UserWriterFavoriteItemDto
    {
        public Guid UserWriterFavoriteId { get; init; }
        public Guid WriterId { get; init; }
        public Guid UserId { get; init; }
    }
}
