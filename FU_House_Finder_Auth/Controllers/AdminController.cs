using FU_House_Finder_Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FU_House_Finder_Auth.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("landlords-pending")]
        public async Task<IActionResult> GetPendingLandlords()
        {
            var landlords = await _adminService.GetPendingLandlordsAsync();
            return Ok(landlords);
        }
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveLandlord(int id)
        {
            try
            {
                await _adminService.ApproveLandlordAsync(id);
                return Ok(new { message = "Landlord approved successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("/api/users/{id}")]
        public async Task<IActionResult> GetUserPublic(int id)
        {
            var user = await _adminService.GetUserPublicAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
