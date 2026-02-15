using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public class CatgoryRepository : ICatgoryRepository
    {


        private readonly LotteryContext _context;


        public CatgoryRepository(LotteryContext context)
        {
            _context = context;
        }

        public async Task<List<Catgories>> GetAllCatgories()
        {
            return await _context.Categories
                .Include(c => c.Gifts)
                .ToListAsync();
        }

        public async Task<Catgories?> GetByICatgory(int id)
        {
            return await _context.Categories
                .Include(c => c.Gifts)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Catgories> CreateCatgory(Catgories category)
        {

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Catgories?> UpdateCatgory(Catgories category)
        {
            var existing = await _context.Categories.FindAsync(category.Id);
            if (existing == null)
                return null;

            existing.Name = category.Name;
            existing.Gifts = category.Gifts;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteCatgory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsCatgory(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
