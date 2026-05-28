using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Interfaces;
using System.Security.Claims;

namespace ProjectManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public IActionResult GetMe()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claim, out int userId))
                return Unauthorized("User ID not found in token.");

            var user = _userService.Get(userId);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpDelete("me")]
        public IActionResult DeleteMe()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claim, out int userId))
                return Unauthorized("User ID not found in token.");

            var result = _userService.Delete(userId);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }
    }
}
