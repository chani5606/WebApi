namespace ProjectAPI.Interfaces
{
    public interface IReportRepository
    {
        Task<int> GetTotalIncome();
        Task SendWinnerEmail(string toEmail, string winnerName, string giftName);

    }
}