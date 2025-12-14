using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetRoomByIdAsync(int id);

        Task<Room> CreateRoomAsync(Room room);
        Task<Room?> UpdateRoomAsync(int id, Room room);
        Task<bool> DeleteRoomAsync(int id);


    }
}
