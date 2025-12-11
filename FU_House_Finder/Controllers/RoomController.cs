using FU_House_Finder.DTO;
using FU_House_Finder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FU_House_Finder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng" });
            }

            return Ok(room);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] CreateRoomDto createRoomDto)
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

                var room = await _roomService.CreateRoomAsync(createRoomDto, landlordId);
                return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
        public async Task<ActionResult<RoomDto>> UpdateRoom(int id, [FromBody] UpdateRoomDto updateRoomDto)
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

                var room = await _roomService.UpdateRoomAsync(id, updateRoomDto, landlordId);

                if (room == null)
                {
                    return NotFound(new { message = "Không tìm thấy phòng" });
                }

                return Ok(room);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = ex.Message // hoặc "Bạn không có quyền"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
