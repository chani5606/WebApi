using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectAPI.Data;

public class LotteryContextFactory
    : IDesignTimeDbContextFactory<LotteryContext>
{
    public LotteryContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LotteryContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;Database=LotteryDB;Trusted_Connection=True;TrustServerCertificate=True"
        );

        return new LotteryContext(optionsBuilder.Options);
    }
}
