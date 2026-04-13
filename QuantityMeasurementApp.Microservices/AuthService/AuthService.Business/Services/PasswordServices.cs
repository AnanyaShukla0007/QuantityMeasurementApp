namespace AuthService.Business.Services;

public class PasswordServices
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}