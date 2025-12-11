using FU_House_Finder.DTO;
using FU_House_Finder.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}