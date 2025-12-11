using FU_House_Finder.DTO;
using FU_House_Finder.Repositories;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<List<HouseDto>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice)
        {
            var houses = await _houseRepository.GetAllHousesAsync(keyword, minPrice, maxPrice);
            
            return houses.Select(h => MapToHouseDto(h)).ToList();
        }

        public async Task<HouseDetailDto?> GetHouseDetailAsync(int id)
        {
            var house = await _houseRepository.GetHouseDetailAsync(id);

            if (house == null)
            {
                return null;
            }

            return MapToHouseDetailDto(house);
        }

        public async Task<HouseDto> CreateHouseAsync(CreateHouseDto createHouseDto, string landlordId)
        {
            if (!int.TryParse(landlordId, out int parsedLandlordId))
            {
                throw new UnauthorizedAccessException("ID người dùng không hợp lệ");
            }

            var house = new House
            {
                LandlordId = parsedLandlordId,
                Name = createHouseDto.Name,
                Address = createHouseDto.Address,
                Description = createHouseDto.Description,
                CampusName = createHouseDto.CampusName,
                PowerPrice = createHouseDto.PowerPrice,
                WaterPrice = createHouseDto.WaterPrice,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var createdHouse = await _houseRepository.CreateHouseAsync(house);
            return MapToHouseDto(createdHouse);
        }

        public async Task<HouseDto?> UpdateHouseAsync(int id, UpdateHouseDto updateHouseDto, string landlordId)
        {
            if (!int.TryParse(landlordId, out int parsedLandlordId))
            {
                throw new UnauthorizedAccessException("ID người dùng không hợp lệ");
            }

            var house = await _houseRepository.GetHouseByIdAsync(id);

            if (house == null)
            {
                return null;
            }

            if (house.LandlordId != parsedLandlordId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền cập nhật nhà trọ này");
            }

            house.Name = updateHouseDto.Name;
            house.Address = updateHouseDto.Address;
            house.Description = updateHouseDto.Description;
            house.CampusName = updateHouseDto.CampusName;
            house.PowerPrice = updateHouseDto.PowerPrice;
            house.WaterPrice = updateHouseDto.WaterPrice;

            var updatedHouse = await _houseRepository.UpdateHouseAsync(house);
            return MapToHouseDto(updatedHouse);
        }

        public async Task<bool> DeleteHouseAsync(int id, string landlordId)
        {
            if (!int.TryParse(landlordId, out int parsedLandlordId))
            {
                throw new UnauthorizedAccessException("ID người dùng không hợp lệ");
            }

            var house = await _houseRepository.GetHouseByIdAsync(id);

            if (house == null)
            {
                throw new KeyNotFoundException("Không tìm thấy nhà trọ");
            }

            if (house.LandlordId != parsedLandlordId)
            {
                throw new UnauthorizedAccessException("Bạn không có quyền xóa nhà trọ này");
            }

            return await _houseRepository.DeleteHouseAsync(id);
        }

        private static HouseDto MapToHouseDto(House house)
        {
            return new HouseDto
            {
                Id = house.Id,
                Name = house.Name,
                Address = house.Address,
                Description = house.Description,
                CampusName = house.CampusName,
                PowerPrice = house.PowerPrice,
                WaterPrice = house.WaterPrice,
                CreatedDate = house.CreatedDate
            };
        }

        private static HouseDetailDto MapToHouseDetailDto(House house)
        {
            return new HouseDetailDto
            {
                Id = house.Id,
                Name = house.Name,
                Address = house.Address,
                Description = house.Description,
                CampusName = house.CampusName,
                PowerPrice = house.PowerPrice,
                WaterPrice = house.WaterPrice,
                CreatedDate = house.CreatedDate,
                Rooms = house.Rooms.Select(r => MapToRoomDto(r)).ToList()
            };
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
