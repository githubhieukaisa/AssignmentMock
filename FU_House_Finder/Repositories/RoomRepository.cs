using FU_House_Finder.Repositories.Context;
using FU_House_Finder.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FU_House_Finder.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);

            if (existingRoom == null)
            {
                return null;
            }

            existingRoom.Name = room.Name;
            existingRoom.Price = room.Price;
            existingRoom.Area = room.Area;
            existingRoom.MaxPeople = room.MaxPeople;
            existingRoom.Status = room.Status;
            existingRoom.Description = room.Description;

            _context.Rooms.Update(existingRoom);
            await _context.SaveChangesAsync();
            return existingRoom;
        }
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null) return false;

            _context.Rooms.Remove(existingRoom);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
