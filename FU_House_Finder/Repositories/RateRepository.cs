using FU_House_Finder.Repositories.Context;
using FU_House_Finder.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FU_House_Finder.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _context;

        public RateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rate>> GetRatesByHouseIdAsync(int houseId)
        {
            return await _context.Rates
                .Where(r => r.HouseId == houseId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<Rate> CreateRateAsync(Rate rate)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
            return rate;
        }
    }
}
