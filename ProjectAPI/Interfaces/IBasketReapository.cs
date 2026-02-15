using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface IBasketReapository
    {
        Task<Basket> CreateBasket(Basket bsk);
        Task<bool> DeleteBasket(int id);
        Task<List<Basket>> GetAllBaskets();
        Task<List<Basket?>> GetBasketById(int id);
        Task<List<Basket?>> GetPurchasesById(int id);

        Task<bool> UpdateBasket(Basket bsk);
        Task<bool> DeleteByGift(int giftId, int userId);
    }
}