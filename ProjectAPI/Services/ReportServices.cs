using Microsoft.Extensions.Logging;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<ReportServices> _logger;

        public ReportServices(IReportRepository reportRepository, ILogger<ReportServices> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        public async Task<int> GetTotalIncome()
        {
            _logger.LogInformation("Calculating total income...");
            var totalIncome = await _reportRepository.GetTotalIncome();
            _logger.LogInformation("Total income calculated: {TotalIncome}", totalIncome);
            return totalIncome;
        }

        public async Task SendWinnerEmail(string toEmail, string winnerName, string giftName)
        {
            _logger.LogInformation("Sending winner email to {Email} for winner {Winner} and gift {Gift}", toEmail, winnerName, giftName);
            await _reportRepository.SendWinnerEmail(toEmail, winnerName, giftName);
            _logger.LogInformation("Email sent successfully to {Email}", toEmail);
        }
    }
}
