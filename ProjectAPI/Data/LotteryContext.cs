using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Data
{
    public class LotteryContext:DbContext

    {
         public LotteryContext(DbContextOptions<LotteryContext> options):base(options) { }
        
        public DbSet<Basket> Basket => Set<Basket>();
        public DbSet<Catgories> Categories => Set<Catgories>();
        public DbSet<Donors> Donors => Set<Donors>();
        public DbSet<Gifts> Gifts => Set<Gifts>();  
        public DbSet<Purchasers> Purchasers => Set<Purchasers>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Catgories>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.Purchasers)
                .WithMany(p => p.Gifts)
                .HasForeignKey(b => b.PurchasersId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.Gifts)
                .WithMany(g => g.baskets)
                .HasForeignKey(b => b.GiftsId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
