using FU_House_Finder.DTO;

namespace FU_House_Finder.Services
{
    public interface IHouseService
    {
        Task<List<HouseDto>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice);
        Task<HouseDetailDto?> GetHouseDetailAsync(int id);
        Task<HouseDto> CreateHouseAsync(CreateHouseDto createHouseDto, string landlordId);
        Task<HouseDto?> UpdateHouseAsync(int id, UpdateHouseDto updateHouseDto, string landlordId);
        Task<bool> DeleteHouseAsync(int id, string landlordId);
    }
}
