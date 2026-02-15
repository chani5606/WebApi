using ProjectAPI.DTOs;
using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface IBasketServices
    {
        Task<BasketResponseDTO> CreateBasket(BasketCreateDTO bsk);
        Task<bool> DeleteBasket(int id);
        Task<List<BasketResponseDTO>> GetAllBaskets();
        Task<List<BasketResponseDTO?>> GetBasketById(int id);
        Task<List<BasketResponseDTO?>> GetPurchasesById(int id);
        Task<bool> UpdateBasket(BasketUpdateDTO bsk, int id);
        Task<bool> DeleteByGift(int giftId, int userId);

    }
}