using Azure.Core.GeoJson;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Services
{
    public class SaleServices : ISaleServices
    {
        private readonly ISaleReapository _saleReapository;
        private readonly ILogger _logger;

        public SaleServices(ISaleReapository saleReapository, ILogger<SaleServices> logger)
        {
            _saleReapository = saleReapository;
            _logger = logger;
        }

        public async Task<Sale> CreateSale(Sale sale)
        {
            _logger.LogInformation("Creating new sale");
            var createdSale = await _saleReapository.CreateSale(sale);
            _logger.LogInformation("Sale created with ID {Id}", createdSale.Id);
            return createdSale;
        }

        public async Task<Sale> GetSales()
        {
            _logger.LogInformation("Retrieving all sales");
            var listSales = await _saleReapository.GetSales();
            return listSales;
        }
        public async Task<Sale> UpdateSale(Sale sale)
        {
            _logger.LogInformation("Updating sale with ID {Id}", sale.Id);
            var updatedSale = await _saleReapository.UpdateSale(sale);

            if (updatedSale == null)
            {
                _logger.LogWarning("Sale with ID {Id} not found for update", sale.Id);
                throw new KeyNotFoundException("Sale not found");
            }

            _logger.LogInformation("Sale with ID {Id} updated successfully", updatedSale.Id);
            return updatedSale;
        }
        public async Task<bool> IsSaleOpen()
        {
            _logger.LogInformation("Checking if sale is open");
            var isOpen = await _saleReapository.IsSaleOpen();
            _logger.LogInformation("Sale is currently {Status}", isOpen ? "open" : "closed");
            return isOpen;
        }

        public async Task<bool> resertSale()
        {
            _logger.LogInformation("Resetting sale");
            var result = await _saleReapository.resertSale();
            _logger.LogInformation("Sale reset {Result}", result ? "successful" : "failed");
            return result;
        }
    }
}
