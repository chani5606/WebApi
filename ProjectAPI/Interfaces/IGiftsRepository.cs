using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public interface IGiftRepository
    {
        Task<Gifts> CreateGift(Gifts gift);
        Task<List<Gifts>> GetAllGifts();
        Task<Gifts?> GetGiftsByID(int id);
        Task<Gifts?> UpdateGifts(Gifts gift);
        Task<bool> DeleteGifts(int id); 
        Task<Gifts?> FindGiftByName(string name);
    }
}
