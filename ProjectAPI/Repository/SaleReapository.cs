using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using System.Runtime.CompilerServices;

namespace ProjectAPI.Repository
{
    public class SaleReapository : ISaleReapository
    {
        private readonly LotteryContext context;


        public SaleReapository(LotteryContext _context)
        {
            context = _context;
        }
        public async Task<Sale> CreateSale(Sale sale)
        {
            context.Sale.Add(sale);
            await context.SaveChangesAsync();
            return sale;
        }
        public async Task<Sale> GetSales()
        {
            var listSales = await context.Sale.FirstOrDefaultAsync();
            return listSales;
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            var existingSale = await context.Sale
                .FirstOrDefaultAsync(s => s.Id == sale.Id);

            if (existingSale == null)
                return null;

            existingSale.endDate = sale.endDate;

            try
            {
                await context.SaveChangesAsync();
                return existingSale;
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message;
                throw new Exception(msg);
            }
        }
        

        public async Task<bool> IsSaleOpen()
        {

            var sale = await context.Sale.FirstOrDefaultAsync();
            if (sale == null)
                return false;

            if (!sale.startDate.HasValue || !sale.endDate.HasValue)
                return false;

            var now = DateTime.UtcNow;

            return now >= sale.startDate.Value && now <= sale.endDate.Value;
        }
        public  async Task<bool> resertSale()
        {
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Basket");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Winner");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Sale");

            return true;
        }
    }
}
