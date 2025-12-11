namespace FU_House_Finder.Services
{
    public class HouseService : IHouseService
    {
        public Task<List<HouseDto>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<HouseDetailDto?> GetHouseDetailAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
