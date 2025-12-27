
using ProjectAPI.Dto;
using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface IGiftsServices
    {
        Task<GifttResponseDTOs> CreateGift(GiftCreateDTOs dto);
        Task<List<GifttResponseDTOs>> GetAllGifts();
        Task<GifttResponseDTOs?> GetGiftsByID(int id);
        Task<GiftUpdateDTOs?> UpdateGifts(GiftUpdateDTOs gift);
        Task<bool> DeleteGifts(int id);
        Task<GifttResponseDTOs?> FindGiftByEmail(string email);
    }
}
