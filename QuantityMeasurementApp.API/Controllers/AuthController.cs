using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using QuantityMeasurementApp.API.Services;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Services;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _config;

        public AuthController(
            UserRepository userRepo,
            PasswordService passwordService,
            JwtService jwtService,
            IConfiguration config)
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            var existing = _userRepo.GetByUsername(request.Username);
            if (existing != null)
                return BadRequest("User already exists");

            var (hash, salt) = _passwordService.HashPassword(request.Password);

            var user = new UserEntity
            {
                Username = request.Username,
                PasswordHash = hash,
                Salt = salt,
                Role = request.Role ?? "User"
            };

            _userRepo.Add(user);

            return Ok("Registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            var user = _userRepo.GetByUsername(request.Username);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (user.PasswordHash == null || user.Salt == null)
                return Unauthorized("Use Google login");

            bool valid = _passwordService.Verify(
                request.Password,
                user.PasswordHash,
                user.Salt);

            if (!valid)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Username, user.Role);
            return Ok(new { token });
        }

        // GOOGLE LOGIN START
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Auth");

            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // GOOGLE CALLBACK (FIXED)
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            if (!result.Succeeded)
                return BadRequest("Google authentication failed");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return BadRequest("Email not found");

            var user = _userRepo.GetByUsername(email);

            if (user == null)
            {
                user = new UserEntity
                {
                    Username = email,
                    PasswordHash = null,
                    Salt = null,
                    Role = "User"
                };

                _userRepo.Add(user);
            }

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            var frontendUrl = _config["FrontendUrl"] ?? "http://localhost:4200";

            return Redirect($"{frontendUrl.TrimEnd('/')}/google-success?token={token}");
        }
    }
}