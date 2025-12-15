using FU_House_Finder.DTO;
using FU_House_Finder.Repositories;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHouseRepository _houseRepository;

        public RoomService(IRoomRepository roomRepository, IHouseRepository houseRepository)
        {
            _roomRepository = roomRepository;
            _houseRepository = houseRepository;
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

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto, string landlordId)
        {
            // Verify that the landlord owns this house
            var house = await _houseRepository.GetHouseByIdAsync(createRoomDto.HouseId);

            if (house == null)
            {
                throw new KeyNotFoundException($"Nhà có id {createRoomDto.HouseId} không tồn tại");
            }

            if (house.LandlordId.ToString() != landlordId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thêm phòng vào nhà này");
            }

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

        public async Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto, string landlordId)
        {
            // Get existing room
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);

            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Phòng có id {id} không tồn tại");
            }

            // Verify that the landlord owns the house that contains this room
            var house = await _houseRepository.GetHouseByIdAsync(existingRoom.HouseId);

            if (house == null)
            {
                throw new KeyNotFoundException($"Nhà có id {existingRoom.HouseId} không tồn tại");
            }

            if (house.LandlordId.ToString() != landlordId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền cập nhật phòng này");
            }

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
        public async Task<bool> DeleteRoomAsync(int id, string landlordId)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Phòng có id {id} không tồn tại");
            }

            var house = await _houseRepository.GetHouseByIdAsync(existingRoom.HouseId);
            if (house == null)
            {
                throw new KeyNotFoundException($"Nhà có id {existingRoom.HouseId} không tồn tại");
            }

            if (house.LandlordId.ToString() != landlordId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xóa phòng này");
            }

            return await _roomRepository.DeleteRoomAsync(id);
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
