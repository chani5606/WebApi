using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly LotteryContext context;
        public WinnerRepository(LotteryContext context)
        {
            this.context = context;
        }
        public async Task<Winner> Create(Winner w)
        {
            context.Winner.Add(w);
            await context.SaveChangesAsync();
            return w;
        }
        public async Task<List<Winner>> GetAll()
        {
            var listWinners = await context.Winner
                .Include(b => b.Gift)
                .Include(b => b.User)
                .ToListAsync();
            return listWinners;
        }
        public async Task<Winner?> GetById(int id)
        {
            var w = await context.Winner
             .FirstOrDefaultAsync(w => w.GiftId == id);
            return w;
        }

    }
}
