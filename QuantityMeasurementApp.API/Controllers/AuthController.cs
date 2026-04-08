using Microsoft.AspNetCore.Mvc;
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

        public AuthController(
            UserRepository userRepo,
            PasswordService passwordService,
            JwtService jwtService)
        {
            _userRepo = userRepo;
            _passwordService = passwordService;
            _jwtService = jwtService;
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
                return Unauthorized("Invalid credentials");

            bool valid = _passwordService.Verify(
                request.Password,
                user.PasswordHash,
                user.Salt);

            if (!valid)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Username, user.Role);
            return Ok(new { token });
        }
    }
}
