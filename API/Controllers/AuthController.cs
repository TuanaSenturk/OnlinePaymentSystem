using Microsoft.AspNetCore.Mvc;
using OnlinePaymentSystem.Business.Services;
using OnlinePaymentSystem.API.DTOs;

namespace OnlinePaymentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Surname) ||
                string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("All fields are required.");
            }
            try
            {
                _userService.createUser(request.Name, request.Surname, request.Email, request.Password);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }
            try
            {
                bool isAuthenticated = _userService.loginUser(request.Email, request.Password);
                if (isAuthenticated)
                {
                    return Ok("Login successful.");
                }
                else
                {
                    return Unauthorized("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
