using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data
{
    public class LotteryContextFactory
    {
        private const string ConnectionString = "Server=localhost;DataBase=WebApiProject" +
            ";Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;";

            public static LotteryContext CreateContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<LotteryContext>();
            optionBuilder.UseSqlServer(ConnectionString);
            return new LotteryContext(optionBuilder.Options);
        }
    }
}
