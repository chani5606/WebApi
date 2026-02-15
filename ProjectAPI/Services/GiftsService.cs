using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectFinal.Interfaces;

namespace ProjectAPI.Services
{
    public class GiftsService : IGiftsServices
    {
        private readonly IGiftRepository _giftRepository;
        private readonly ICatgoryRepository _catgoryRepository;
        private readonly IMapper _mapper;
        private readonly IDonorRepository _donorRepository;
        private readonly ILogger<GiftsService> _logger;

        public GiftsService(
            IGiftRepository giftRepository,
            IMapper mapper,
            ICatgoryRepository catgoryRepository,
            IDonorRepository donorRepository,
            ILogger<GiftsService> logger)
        {
            _giftRepository = giftRepository;
            _mapper = mapper;
            _catgoryRepository = catgoryRepository;
            _donorRepository = donorRepository;
            _logger = logger;
        }

        public async Task<GifttResponseDTOs> CreateGift(GiftCreateDTOs dto)
        {
            _logger.LogInformation("Creating gift with Name {Name}", dto.Name);

            if (!await _catgoryRepository.ExistsCatgory(dto.CatgoryId))
            {
                _logger.LogWarning("Category with ID {Id} not found", dto.CatgoryId);
                throw new KeyNotFoundException("Category not found");
            }

            if (await _donorRepository.GetDonorById(dto.DonorId) == null)
            {
                _logger.LogWarning("Donor with ID {Id} not found", dto.DonorId);
                throw new KeyNotFoundException("Donor not found");
            }

            var giftEntity = _mapper.Map<Gifts>(dto);
            var createdGift = await _giftRepository.CreateGift(giftEntity);

            _logger.LogInformation("Gift created with ID {Id}", createdGift.Id);
            return _mapper.Map<GifttResponseDTOs>(createdGift);
        }

        public async Task<List<GifttResponseDTOs>> GetAllGifts()
        {
            _logger.LogInformation("Retrieving all gifts");
            var listGifts = await _giftRepository.GetAllGifts();
            _logger.LogInformation("Retrieved {Count} gifts", listGifts.Count);
            return listGifts.Select(gift => _mapper.Map<GifttResponseDTOs>(gift)).ToList();
        }

        public async Task<GifttResponseDTOs?> GetGiftsByID(int id)
        {
            _logger.LogInformation("Retrieving gift with ID {Id}", id);
            var gift = await _giftRepository.GetGiftsByID(id);
            if (gift == null)
                _logger.LogWarning("Gift with ID {Id} not found", id);
            return gift != null ? _mapper.Map<GifttResponseDTOs>(gift) : null;
        }

        public async Task<GifttResponseDTOs?> UpdateGifts(GiftUpdateDTOs dto, int id)
        {
            _logger.LogInformation("Updating gift with ID {Id}", id);
            var existingGift = await _giftRepository.GetGiftsByID(id);
            if (existingGift == null)
            {
                _logger.LogWarning("Gift with ID {Id} not found", id);
                return null;
            }

            if (!await _catgoryRepository.ExistsCatgory(dto.CatgoryId))
            {
                _logger.LogWarning("Category with ID {Id} not found", dto.CatgoryId);
                throw new Exception("Category does not exist");
            }

            if (await _donorRepository.GetDonorById(dto.DonorId) == null)
            {
                _logger.LogWarning("Donor with ID {Id} not found", dto.DonorId);
                throw new Exception("Donor does not exist");
            }

            if (dto.Name != null) existingGift.Name = dto.Name;
            if (dto.GiftNumber != 0) existingGift.GiftNumber = dto.GiftNumber;
            if (dto.DonorId != 0) existingGift.DonorId = dto.DonorId;
            if (dto.CatgoryId != 0) existingGift.CatgoryId = dto.CatgoryId;
            if (dto.Price != 0) existingGift.Price = dto.Price;
            if (dto.PathImage != null) existingGift.PathImage = dto.PathImage;

            var updatedGift = await _giftRepository.UpdateGifts(existingGift);
            _logger.LogInformation("Gift with ID {Id} updated successfully", id);

            return updatedGift != null ? _mapper.Map<GifttResponseDTOs>(updatedGift) : null;
        }

        public async Task<bool> DeleteGifts(int id)
        {
            _logger.LogInformation("Deleting gift with ID {Id}", id);
            var result = await _giftRepository.DeleteGifts(id);
            if (result)
                _logger.LogInformation("Gift with ID {Id} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete gift with ID {Id}", id);
            return result;
        }

        public async Task<GifttResponseDTOs?> FindGiftByname(string name)
        {
            _logger.LogInformation("Finding gift by name {Name}", name);
            var gift = await _giftRepository.FindGiftByName(name);
            if (gift == null)
            {
                _logger.LogWarning("No gift found with name {Name}", name);
                return null;
            }
            _logger.LogInformation("Gift found: {GiftName} (ID {Id})", gift.Name, gift.Id);
            return _mapper.Map<GifttResponseDTOs>(gift); 
        }
        public async Task<List<GifttResponseDTOs?>> FindGiftByDonor(string d)
        {
            _logger.LogInformation("Finding gifts by donor {Donor}", d);
            var gifts = await _giftRepository.FindGiftByDonor(d);
            if (gifts == null || gifts.Count == 0)
            {
                _logger.LogWarning("No gifts found for donor {Donor}", d);
                return new List<GifttResponseDTOs?>();
            }
            _logger.LogInformation("Found {Count} gifts for donor {Donor}", gifts.Count, d);
            return gifts.Select(gift => _mapper.Map<GifttResponseDTOs?>(gift)).ToList();
        }
    }
}
