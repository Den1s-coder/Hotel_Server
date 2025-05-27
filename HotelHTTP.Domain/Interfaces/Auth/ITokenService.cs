namespace Hotel.Domain.Interfaces.Repo
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role);
    }
}