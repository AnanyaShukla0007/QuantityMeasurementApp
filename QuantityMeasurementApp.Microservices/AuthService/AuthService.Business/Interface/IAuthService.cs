using AuthService.Model.DTOs;

namespace AuthService.Business.Interface;

public interface IAuthService
{
    bool Register(AuthDTO dto);
    string Login(AuthDTO dto);
}