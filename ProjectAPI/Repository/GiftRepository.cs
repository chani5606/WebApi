using ProjectAPI.Data;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Repository
{
    public class GiftRepository
    {
        LotteryContext context = LotteryContextFactory.CreateContext();

        public async Task<Gifts> CreateGift(Gifts gift)
        {
            context.Gifts.Add(gift);
            await context.SaveChangesAsync();
            return gift;
        }

        public async Task<List<Gifts>> GetAllGifts()
        {
            var listGifts = await context.Gifts.Select(x => x).ToListAsync();
            return listGifts;
        }

        public async Task<Gifts?> GetGiftsByID(int id)
        {
            var gift = await context.Gifts.FindAsync(id);
            if (gift == null)
                return null;

            return gift;
        }

        public async Task<Gifts?> UpdateGifts(Gifts gift, int id)
        {
            var existingGift = await context.Gifts.FindAsync(id);
            if (existingGift == null)
                return null;

            existingGift.Name = gift.Name;
            existingGift.IdDonor = gift.IdDonor;
            existingGift.IdCatgory = gift.IdCatgory;
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
    }
}
