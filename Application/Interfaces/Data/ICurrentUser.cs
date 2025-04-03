namespace Application.Interfaces.Data
{
    public interface ICurrentUser
    {
        string? Lang { get; }
        Guid UserId { get; }
        string? Role { get; }
        string? Email { get; }
    }
}