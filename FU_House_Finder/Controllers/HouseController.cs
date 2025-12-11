using FU_House_Finder.DTO;
using FU_House_Finder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FU_House_Finder.Controllers
{
    [Route("api/house")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<HouseDto>>> GetAllHouses(
            [FromQuery] string? keyword,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var houses = await _houseService.GetAllHousesAsync(keyword, minPrice, maxPrice);
            return Ok(houses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDetailDto>> GetHouseDetail(int id)
        {
            var house = await _houseService.GetHouseDetailAsync(id);

            if (house == null)
            {
                return NotFound(new { message = "Không tìm thấy nhà" });
            }

            return Ok(house);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<HouseDto>> CreateHouse([FromBody] CreateHouseDto createHouseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var landlordId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(landlordId))
                {
                    return Unauthorized(new { message = "Không thể xác thực người dùng" });
                }

                var house = await _houseService.CreateHouseAsync(createHouseDto, landlordId);
                return CreatedAtAction(nameof(GetHouseDetail), new { id = house.Id }, house);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid("Bạn không có quyền");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<HouseDto>> UpdateHouse(int id, [FromBody] UpdateHouseDto updateHouseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var landlordId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(landlordId))
                {
                    return Unauthorized(new { message = "Không thể xác thực người dùng" });
                }

                var house = await _houseService.UpdateHouseAsync(id, updateHouseDto, landlordId);

                if (house == null)
                {
                    return NotFound(new { message = "Không tìm thấy nhà" });
                }

                return Ok(house);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHouse(int id)
        {
            try
            {
                var landlordId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(landlordId))
                {
                    return Unauthorized(new { message = "Không thể xác thực người dùng" });
                }

                var result = await _houseService.DeleteHouseAsync(id, landlordId);

                if (!result)
                {
                    return NotFound(new { message = "Không tìm thấy nhà" });
                }

                return Ok(new { message = "Xóa nhà trọ thành công" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}