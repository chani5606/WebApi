using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Services
{
    public class GiftsService: IGiftsServices
    {
        private readonly IGiftRepository giftRepository ;

        public GiftsService(IGiftRepository _giftRepository)
        {
            giftRepository = _giftRepository;
        }
        public async Task<GifttResponseDTOs> CreateGift(GiftCreateDTOs dto)
        {
        //להוסיף בדיקת תקינות לקטגוריה האם קיים כזה ID
        var createGift = new Gifts
                {
                    Name = dto.Name,
                    GiftNumber = dto.GiftNumber,
                    DonorId = dto.IdDonor,
                    CatgoryId = dto.IdCatgory,
                    Price = dto.Price,
                    PathImage = dto.PathImage
                };
            var gift = await giftRepository.CreateGift(createGift);
            return  MapToDTO(gift);
        }

        public async Task<List<GifttResponseDTOs>> GetAllGifts()
        {
            var ListGifts = await giftRepository.GetAllGifts();
            return ListGifts.Select(gift => MapToDTO(gift)).ToList();
        }

        public async Task<GifttResponseDTOs?> GetGiftsByID(int id)
        {
            var gift = await giftRepository.GetGiftsByID(id);
            if (gift == null)
            {
                return null;
            }

            return MapToDTO(gift);
        }

        public async Task<GifttResponseDTOs?> UpdateGifts(GiftUpdateDTOs dto, int id)
        {
            var existingGift = await giftRepository.GetGiftsByID(id);
            if (existingGift == null) return null;

            // עדכון המתנה לפי הנתונים החדשים
            if (dto.Name != null) existingGift.Name = dto.Name;
            if (dto.GiftNumber != 0) existingGift.GiftNumber = dto.GiftNumber;
            if (dto.IdDonor != 0) existingGift.DonorId = dto.IdDonor;
            if (dto.IdCatgory != 0)
                existingGift.CatgoryId = dto.IdCatgory;
            if (dto.Price != 0) existingGift.Price = dto.Price;
            if (dto.PathImage != null) existingGift.PathImage = dto.PathImage;

            var updatedGift = await giftRepository.UpdateGifts(existingGift);
            return updatedGift != null ? MapToDTO(updatedGift) : null;
        }
        public async Task<bool> DeleteGifts(int id)
        {
            return await giftRepository.DeleteGifts(id);
        }

        public static GifttResponseDTOs MapToDTO(Gifts gift)
        {
            return new GifttResponseDTOs
            {
                Name = gift.Name,
                GiftNumber = gift.GiftNumber,
                IdDonor = gift.DonorId,
                IdCatgory = gift.CatgoryId,
                Price = gift.Price,
                PathImage = gift.PathImage
            };
        }

        public Task<GifttResponseDTOs?> FindGiftByname(string name)
        {
            throw new NotImplementedException();
        }

     
    }
}
