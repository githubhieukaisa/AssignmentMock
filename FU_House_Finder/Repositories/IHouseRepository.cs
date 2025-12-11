using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories
{
    public interface IHouseRepository
    {
        Task<List<House>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice);
        Task<House?> GetHouseDetailAsync(int id);
        Task<House?> GetHouseByIdAsync(int id);
        Task<House> CreateHouseAsync(House house);
        Task<House> UpdateHouseAsync(House house);
        Task<bool> DeleteHouseAsync(int id);
    }
}
