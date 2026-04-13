using AuthService.Business.Interface;
using AuthService.Model.DTOs;
using AuthService.Model.Entities;
using AuthService.Repository.Interface;

namespace AuthService.Business.Services;

public class AuthServiceImpl : IAuthService
{
    private readonly IUserRepository _repo;
    private readonly JwtServices _jwt;
    private readonly PasswordServices _password;

    public AuthServiceImpl(IUserRepository repo, JwtServices jwt, PasswordServices password)
    {
        _repo = repo;
        _jwt = jwt;
        _password = password;
    }

    public bool Register(AuthDTO dto)
    {
        if (_repo.GetByUsername(dto.Username) != null) return false;
        _repo.Add(new UserEntity
        {
            Username = dto.Username,
            Password = _password.HashPassword(dto.Password)
        });
        return true;
    }

    public string Login(AuthDTO dto)
    {
        var user = _repo.GetByUsername(dto.Username);
        if (user == null) return string.Empty;
        return _password.VerifyPassword(dto.Password, user.Password)
            ? _jwt.GenerateToken(user.Username)
            : string.Empty;
    }
}