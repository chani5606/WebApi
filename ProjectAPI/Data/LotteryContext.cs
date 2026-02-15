using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace ProjectAPI.Data
{
    public class LotteryContext : DbContext
    {
        public LotteryContext(DbContextOptions<LotteryContext> options)
            : base(options) { }

        public DbSet<Basket> Basket => Set<Basket>();
        public DbSet<Catgories> Categories => Set<Catgories>();
        public DbSet<Donors> Donors => Set<Donors>();
        public DbSet<Gifts> Gifts => Set<Gifts>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Winner> Winner => Set<Winner>();
        public DbSet<Sale> Sale => Set<Sale>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== Categories =====
            modelBuilder.Entity<Catgories>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(e => e.Gifts)
                      .WithOne(e => e.Catgory)
                      .HasForeignKey(e => e.CatgoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ===== Donors =====
            modelBuilder.Entity<Donors>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(15);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Nieghbrhood).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Street).IsRequired().HasMaxLength(200);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasMany(e => e.Gifts)
                      .WithOne(e => e.Donor)
                      .HasForeignKey(e => e.DonorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ===== Gifts =====
            modelBuilder.Entity<Gifts>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.PathImage).IsRequired().HasMaxLength(300);

                entity.HasMany(e => e.baskets)
                      .WithOne(e => e.Gifts)
                      .HasForeignKey(e => e.GiftsId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ===== Users =====
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(15);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Nieghbrhood).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Street).IsRequired().HasMaxLength(200);

                entity.HasIndex(e => e.Email).IsUnique();
            });

            // ===== Basket =====
            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.Gifts)
                .WithMany(g => g.baskets)
                .HasForeignKey(b => b.GiftsId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Winner =====
            modelBuilder.Entity<Winner>(entity =>
            {
                entity.HasKey(w => w.Id);

                entity.Property(w => w.GiftId).IsRequired();
                entity.Property(w => w.UserId).IsRequired();

                // One Gift -> One Winner
                entity.HasOne(w => w.Gift)
                      .WithOne()
                      .HasForeignKey<Winner>(w => w.GiftId)
                      .OnDelete(DeleteBehavior.Restrict);

                // User -> Winners
                entity.HasOne(w => w.User)
                      .WithMany()
                      .HasForeignKey(w => w.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }

    }
}
