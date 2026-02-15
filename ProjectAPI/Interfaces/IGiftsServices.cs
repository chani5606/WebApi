
using ProjectAPI.Dto;
using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface IGiftsServices
    {
        Task<GifttResponseDTOs> CreateGift(GiftCreateDTOs dto);
        Task<List<GifttResponseDTOs>> GetAllGifts();
        Task<GifttResponseDTOs?> GetGiftsByID(int id);
        Task<GifttResponseDTOs?> UpdateGifts(GiftUpdateDTOs dto, int id);
        Task<bool> DeleteGifts(int id);
        Task<GifttResponseDTOs?> FindGiftByname(string name);
        Task<List<GifttResponseDTOs?>> FindGiftByDonor(string d);
    }
}
