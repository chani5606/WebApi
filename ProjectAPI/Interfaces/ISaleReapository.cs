using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface ISaleReapository
    {
        Task<Sale> CreateSale(Sale sale);
        Task<Sale> GetSales();   
            Task<Sale> UpdateSale(Sale sale);
       Task<bool> IsSaleOpen();
       Task<bool> resertSale();
    }
}