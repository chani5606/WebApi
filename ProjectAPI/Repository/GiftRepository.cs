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
            try
            {
                await context.SaveChangesAsync();
                return gift;
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message;
                throw new Exception(msg);
            }
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
           var g = context.Gifts
        .Include(g => g.baskets)
            .ThenInclude(b => b.User)
        .SingleOrDefaultAsync(g => g.Id == id);

            return await g;


        }
        //שיניתי
        public async Task<Gifts?> UpdateGifts(Gifts gift)
        {
            var existingGift = await context.Gifts
                .FirstOrDefaultAsync(g => g.Id == gift.Id);

            if (existingGift == null)
                return null;

            existingGift.Name = gift.Name;
            existingGift.DonorId = gift.DonorId;
            existingGift.CatgoryId = gift.CatgoryId;
            existingGift.Price = gift.Price;
            existingGift.GiftNumber = gift.GiftNumber;

            await context.SaveChangesAsync();

            return await context.Gifts
                .Include(g => g.Catgory)
                .Include(g => g.Donor)
                .FirstOrDefaultAsync(g => g.Id == gift.Id);
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
        public async Task<List<Gifts>> FindGiftByDonor(string d)
        {
            var foundGifts = await context.Gifts
                .Where(g => g.Donor.Name == d)
                .ToListAsync();
            if (foundGifts == null || foundGifts.Count == 0)
            {
                return new List<Gifts>();
            }
            return foundGifts;
        }

        public async Task<List<Gifts>> GetGiftsWithUser()
        {
            var gifts = await context.Gifts
                .Include(u =>  u.baskets)
                .ToListAsync();
            return gifts;
        }

    }
}
