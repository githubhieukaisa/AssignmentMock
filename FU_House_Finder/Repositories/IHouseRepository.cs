using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories
{
    public interface IHouseRepository
    {
        Task<List<House>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice);
        Task<House?> GetHouseDetailAsync(int id);
    }
}
