using Microsoft.EntityFrameworkCore;
using FU_House_Finder.Repositories.Context;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly AppDbContext _context;

        public HouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<House>> GetAllHousesAsync(string? keyword, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Houses.Where(h => !h.IsDeleted);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(h => h.Name.Contains(keyword));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.Price >= minPrice));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.Price <= maxPrice));
            }

            return await query.ToListAsync();
        }

        public async Task<House?> GetHouseDetailAsync(int id)
        {
            return await _context.Houses
                .Where(h => h.Id == id && !h.IsDeleted)
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync();
        }
    }
}