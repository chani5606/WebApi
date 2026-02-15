using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface ISaleServices
    {
        Task<Sale> CreateSale(Sale sale);
        Task<Sale> GetSales();       
        Task<Sale> UpdateSale(Sale sale);
        Task<bool> IsSaleOpen();
        Task<bool> resertSale();
    }
}