using FU_House_Finder.DTO;

namespace FU_House_Finder.Services
{
    public interface IRoomService
    {
        Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto, string landlordId);
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto, string landlordId);
        Task<bool> DeleteRoomAsync(int id, string landlordId);

    }
}
