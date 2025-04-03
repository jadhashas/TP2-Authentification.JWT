using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Services;
using Authentification.JWT.Service.Services.Interfaces;
using Authentification.JWT.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly IJwtService _jwtService;
        public AuthController(UserService userService, IJwtService jwtService)

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
            var user = await _userService.GetEntityByUsernameAsync(model.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var hashed = _userService.HashPassword(model.Password);

            Console.WriteLine($"Entered: {hashed}");
            Console.WriteLine($"Stored : {user.PasswordHash}");

            if (user.PasswordHash != hashed)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user.Id);

            return Ok(new { token });
        }
    }
}
