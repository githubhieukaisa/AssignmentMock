using FU_House_Finder.DTO;
using FU_House_Finder.Repositories;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return null;
            }

            return MapToRoomDto(room);
        }
        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto)
        {
            var room = new Room
            {
                HouseId = createRoomDto.HouseId,
                Name = createRoomDto.Name,
                Price = createRoomDto.Price,
                Area = createRoomDto.Area,
                MaxPeople = createRoomDto.MaxPeople,
                Status = (RoomStatus)createRoomDto.Status,
                Description = createRoomDto.Description
            };

            var createdRoom = await _roomRepository.CreateRoomAsync(room);
            return MapToRoomDto(createdRoom);
        }

        public async Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto)
        {
            var room = new Room
            {
                Name = updateRoomDto.Name,
                Price = updateRoomDto.Price,
                Area = updateRoomDto.Area,
                MaxPeople = updateRoomDto.MaxPeople,
                Status = (RoomStatus)updateRoomDto.Status,
                Description = updateRoomDto.Description
            };

            var updatedRoom = await _roomRepository.UpdateRoomAsync(id, room);

            if (updatedRoom == null)
            {
                return null;
            }

            return MapToRoomDto(updatedRoom);
        }

        private static RoomDto MapToRoomDto(Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Price = room.Price,
                Area = room.Area,
                MaxPeople = room.MaxPeople,
                Status = (int)room.Status,
                Description = room.Description
            };
        }
    }
}
