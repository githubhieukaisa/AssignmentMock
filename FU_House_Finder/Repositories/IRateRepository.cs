using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories
{
    public interface IRateRepository
    {
        Task<IEnumerable<Rate>> GetRatesByHouseIdAsync(int houseId);
        Task<Rate> CreateRateAsync(Rate rate);
    }
}
