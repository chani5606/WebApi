using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ProjectAPI.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly LotteryContext _context;
        private readonly IConfiguration _config;

        public ReportRepository(LotteryContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<int> GetTotalIncome()
        {
            var totalIncome = await _context.Basket
                .SumAsync(b => b.Gifts.Price);

            return totalIncome;
        }

        public async Task SendWinnerEmail(string toEmail, string winnerName, string giftName)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("yourproject@gmail.com"),
                Subject = "ברכות! זכית בהגרלה 🎉",
                Body = $@"
                    שלום {winnerName},

                    שמחים לבשר שזכית במתנה: {giftName}

                    ברכות!
                    "
            };

            mail.To.Add(toEmail);

            var smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]))
            {
                Credentials = new NetworkCredential(
        _config["Smtp:Email"],
        _config["Smtp:Password"]
    ),
                EnableSsl = true,

            };


            await smtp.SendMailAsync(mail);
        }
    }

}
