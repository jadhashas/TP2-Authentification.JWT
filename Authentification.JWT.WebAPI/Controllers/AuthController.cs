using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Services;
using Authentification.JWT.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly JwtService _jwtService;
        public AuthController(UserService userService, JwtService jwtService)

        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userDto = new UserDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            var result = await _userService.RegisterUserAsync(userDto);

            if (result == null)
                return BadRequest("Username or email already exists.");

            return Ok(new { message = "Registration successful", user = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByUsernameAsync(model.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            // Vérification du mot de passe
            var hashed = _userService.HashPassword(model.Password);

            if (user.Password != hashed) // user.Password contient PasswordHash dans le DTO
                return Unauthorized("Invalid username or password.");

            var userId = await _userService.GetUserIdByUsernameAsync(model.Username);
            var token = _jwtService.GenerateToken(userId);

            return Ok(new { token });
        }
    }
}
