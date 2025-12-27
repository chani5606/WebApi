using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Data
{
    public class LotteryContext:DbContext

    {
         public LotteryContext(DbContextOptions<LotteryContext> options):base(options) { }
        
          public DbSet<Adress> Adress => Set<Adress>();
        public DbSet<Basket> Basket => Set<Basket>();
        public DbSet<Catgories> Categories => Set<Catgories>();
        public DbSet<Donors> Donors => Set<Donors>();
        public DbSet<Gifts> Gifts => Set<Gifts>();  
        public DbSet<Purchasers> Purchasers => Set<Purchasers>();   
      
    }
}
