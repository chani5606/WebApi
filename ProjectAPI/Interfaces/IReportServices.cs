namespace ProjectAPI.Interfaces
{
    public interface IReportServices
    {
        Task<int> GetTotalIncome();
        Task SendWinnerEmail(string toEmail, string winnerName, string giftName);

    }
}