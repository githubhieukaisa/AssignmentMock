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
