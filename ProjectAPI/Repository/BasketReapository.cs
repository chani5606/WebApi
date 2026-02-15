using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public class BasketReapository : IBasketReapository
    {
        private readonly LotteryContext _context;

        public BasketReapository(LotteryContext context)
        {
            _context = context;
        }
        public async Task<Basket> CreateBasket(Basket bsk)
        {
            _context.Basket.Add(bsk);
            await _context.SaveChangesAsync();
            return bsk;
        }
        public async Task<List<Basket>> GetAllBaskets()
        {
            var baskets = await _context.Basket
          .Include(b => b.User)
          .Include(b => b.Gifts)
              .ThenInclude(g => g.Catgory)
          .Include(b => b.Gifts)
              .ThenInclude(g => g.Donor)
           //.Where(b => b.Status == 1)
          .ToListAsync();

            return baskets;
        }

        public async Task<List<Basket?>> GetBasketById(int id)
        {
            var baskets = await _context.Basket
        .Include(b => b.User)
        .Include(b => b.Gifts)
            .ThenInclude(g => g.Catgory)
        .Include(b => b.Gifts)
            .ThenInclude(g => g.Donor)
        //.Where(b => b.UserId == id && b.Status == 0)
        .ToListAsync();
            return baskets;  
        }
        public async Task<List<Basket?>> GetPurchasesById(int id)
        {
            var baskets = await _context.Basket
         .Include(b => b.User)
         .Include(b => b.Gifts)
             .ThenInclude(g => g.Catgory)
         .Include(b => b.Gifts)
             .ThenInclude(g => g.Donor)
             .Where(b => b.Id == id && b.Status == 1).ToListAsync();
            return baskets;
        }
        public async Task<bool> UpdateBasket(Basket bsk)
        {
            var existingBasket = await _context.Basket.FindAsync(bsk.Id);
            if (existingBasket == null)
                return false;
            existingBasket.Status = bsk.Status;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating basket: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteBasket(int id)
        {
            var basket = await _context.Basket.FindAsync(id);
            if (basket == null)
                return false;

            _context.Basket.Remove(basket);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteByGift(int giftId, int userId)
        {
            var rows = await _context.Basket
           .Where(b => b.UserId == userId && b.GiftsId == giftId)
           .ToListAsync();

            if (!rows.Any()) return false;

            _context.Basket.RemoveRange(rows);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
