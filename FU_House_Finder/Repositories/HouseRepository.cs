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

        public async Task<House?> GetHouseByIdAsync(int id)
        {
            return await _context.Houses
                .Where(h => h.Id == id && !h.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<House> CreateHouseAsync(House house)
        {
            _context.Houses.Add(house);
            await _context.SaveChangesAsync();
            return house;
        }

        public async Task<House> UpdateHouseAsync(House house)
        {
            _context.Houses.Update(house);
            await _context.SaveChangesAsync();
            return house;
        }

        public async Task<bool> DeleteHouseAsync(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return false;
            }

            house.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}