using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Services
{
    public interface IRateService
    {
        Task<IEnumerable<object>> GetRatesByHouseIdAsync(int houseId);
        Task<Rate> CreateRateAsync(Rate rate);
    }
}
