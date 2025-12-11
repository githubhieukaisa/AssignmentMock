using FU_House_Finder.DTO;

namespace FU_House_Finder.Services
{
    public interface IHouseService
    {
        Task<List<HouseDto>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice);
        Task<HouseDetailDto?> GetHouseDetailAsync(int id);
    }
}
