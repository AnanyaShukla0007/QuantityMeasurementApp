using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.API.Services;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Services;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
        public IActionResult Register(RegisterRequest request)
        {
            var existing = _userRepo.GetByUsername(request.Username);

            if (existing != null)
                return BadRequest("User already exists");

            var (hash, salt) = _passwordService.HashPassword(request.Password);

            var user = new UserEntity
            {
                Username = request.Username,
                PasswordHash = hash,
                Salt = salt,
                Role = "User"
            };

            _userRepo.Add(user);

            return Ok("Registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _userRepo.GetByUsername(request.Username);

            if (user == null)
                return Unauthorized("Invalid username");

            bool valid = _passwordService.Verify(
                request.Password,
                user.PasswordHash,
                user.Salt);

            if (!valid)
                return Unauthorized("Invalid password");

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            return Ok(new { token });
        }
    }
}