using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Services;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto, UserRole.Student);
                return Ok(new { message = "User registered successfully", user });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration", error = ex.Message });
            }
        }
    }
}
