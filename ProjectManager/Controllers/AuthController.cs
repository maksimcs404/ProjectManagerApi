using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;
using ProjectManager.Core.Models.Domain;

namespace ProjectManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IUserService _userService { get; }
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserRequest createUserRequest)
        {
            var response = _userService.Create(createUserRequest);
            if (response.IsSuccess)
                return Ok(response.Data);
            else
                return BadRequest(response.Error);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _userService.GetByUsername(loginRequest.Username);
            if (user != null)
            {
                if (user.Password == loginRequest.Password)
                {
                    var token = JwtService.GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid password");
                }
            }
            else
            {
                return BadRequest("User not found");

            }
        }
    }
}
