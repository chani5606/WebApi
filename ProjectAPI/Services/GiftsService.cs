using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Services
{
    public class GiftsService
    {
        private readonly GiftRepository giftRepository = new();

        public async Task<Gifts> CreateGift(GiftCreateDto dto)
        {
            var gift = new Gifts
            {
                Name = dto.Name,
                GiftNumber = dto.GiftNumber,
                IdDonor = dto.IdDonor,
                IdCatgory = dto.IdCatgory,
                Price = dto.Price,
                PathImage = dto.PathImage
            };

            return await giftRepository.CreateGift(gift);
        }

        public async Task<List<Gifts>> GetAllGifts()
        {
            return await giftRepository.GetAllGifts();
        }

        public async Task<Gifts> GetGiftsByID(int id)
        {
            return await giftRepository.GetGiftsByID(id);
        }

        public async Task<Gifts> UpdateGifts(GiftCreateDto dto, int id)
        {
            var gift = new Gifts
            {
                Name = dto.Name,
                GiftNumber = dto.GiftNumber,
                IdDonor = dto.IdDonor,
                IdCatgory = dto.IdCatgory,
                Price = dto.Price,
                PathImage = dto.PathImage
            };

            return await giftRepository.UpdateGifts(gift, id);
        }

        public async Task<bool> DeleteGifts(int id)
        {
            return await giftRepository.DeleteGifts(id);
        }
    }
}
