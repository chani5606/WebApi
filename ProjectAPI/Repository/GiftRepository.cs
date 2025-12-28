using ProjectAPI.Data;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Repository
{
    public class GiftRepository : IGiftRepository
    {
        private readonly LotteryContext context;

        public GiftRepository(LotteryContext _context)
        {
            context = _context; 
        }

        public async Task<Gifts> CreateGift(Gifts gift)
        {
            context.Gifts.Add(gift);
            await context.SaveChangesAsync();
            return gift;
        }

        public async Task<List<Gifts>> GetAllGifts()
        {
            var listGifts = await context.Gifts.
                Include(c => c.Catgory).
                Include(c => c.Donor).
                ToListAsync();

            return listGifts;
        }

        public async Task<Gifts?> GetGiftsByID(int id)
        {
            var gift = await context.Gifts.
                FirstOrDefaultAsync(c => c.Id == id);
          

            return gift;
        }

        public async Task<Gifts?> UpdateGifts(Gifts gift)
        {
            var existingGift = await context.Gifts.FindAsync(gift.Id);
            if (existingGift == null)
                return null;

            existingGift.Name = gift.Name;
            existingGift.DonorId = gift.DonorId;
            existingGift.CatgoryId = gift.CatgoryId;
            existingGift.Price = gift.Price;
            existingGift.GiftNumber = gift.GiftNumber;

            await context.SaveChangesAsync();
            return existingGift;
        }

        public async Task<bool> DeleteGifts(int id)
        {
            var existingGift = await context.Gifts.FindAsync(id);
            if (existingGift == null)
                return false;

            context.Gifts.Remove(existingGift);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Gifts?> FindGiftByName(string name)
        {
            var foundGift = await context.Gifts.FirstOrDefaultAsync(g => g.Name == name);
            return  foundGift;
        }
    }
}
