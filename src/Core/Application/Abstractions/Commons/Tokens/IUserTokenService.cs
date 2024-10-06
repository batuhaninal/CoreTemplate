namespace Application.Abstractions.Commons.Tokens
{
    public interface IUserTokenService
    {
        public Guid UserId { get; }
        public Guid WriterId { get; }
        public string UserEmail { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }
    }
}
