using AuthService.Business.Interface;
using AuthService.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(AuthDTO dto)
    {
        return _authService.Register(dto)
            ? Ok(new { message = "Registered successfully" })
            : BadRequest(new { message = "User already exists" });
    }

    [HttpPost("login")]
    public IActionResult Login(AuthDTO dto)
    {
        var token = _authService.Login(dto);
        return string.IsNullOrEmpty(token)
            ? Unauthorized(new { message = "Invalid credentials" })
            : Ok(new { token });
    }
}