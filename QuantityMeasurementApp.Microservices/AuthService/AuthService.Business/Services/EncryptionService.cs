using System.Security.Cryptography;
using System.Text;
namespace AuthService.Business.Services;
public class EncryptionService
{
    public string ComputeSha256(string value)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes);
    }
}