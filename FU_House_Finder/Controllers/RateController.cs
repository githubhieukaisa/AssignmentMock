using FU_House_Finder.DTO;
using FU_House_Finder.Repositories.Models;
using FU_House_Finder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FU_House_Finder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("house/{houseId}")]
        public async Task<IActionResult> GetRatesByHouse(int houseId)
        {
            var rates = await _rateService.GetRatesByHouseIdAsync(houseId);
            return Ok(rates);
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> CreateRate([FromBody] CreateRateDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.Star < 1 || dto.Star > 5) return BadRequest(new { message = "Star must be between 1 and 5." });

            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var studentId)) return Unauthorized(new { message = "Invalid token user id." });

            var rate = new Rate
            {
                HouseId = dto.HouseId,
                StudentId = studentId,
                Star = dto.Star,
                Comment = dto.Comment ?? string.Empty,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _rateService.CreateRateAsync(rate);

            return CreatedAtAction(nameof(GetRatesByHouse), new { houseId = created.HouseId }, new { message = "Rate created", id = created.Id });
        }

        
    }
}

