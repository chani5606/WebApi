using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketReapository _basketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketServices> _logger;
        private readonly ISaleReapository _saleReapository;


        public BasketServices(
            IBasketReapository basketRepository,
            IUserRepository userRepository,
            IGiftRepository giftRepository,
            IMapper mapper,
            ILogger<BasketServices> logger,
            ISaleReapository saleReapository)
        {
            _basketRepository = basketRepository;
            _userRepository = userRepository;
            _giftRepository = giftRepository;
            _mapper = mapper;
            _logger = logger;
            _saleReapository = saleReapository;
        }

        public async Task<BasketResponseDTO> CreateBasket(BasketCreateDTO bsk)
        {
            if (!await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to create basket while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot create basket.");
            }

            _logger.LogInformation("Creating basket for UserId {UserId} and GiftId {GiftId}", bsk.UserId, bsk.GiftsId);

            if (await _userRepository.GetUsersByID(bsk.UserId) == null)
            {
                _logger.LogWarning("User with ID {UserId} does not exist", bsk.UserId);
                throw new KeyNotFoundException("User does not exist");
            }

            if (await _giftRepository.GetGiftsByID(bsk.GiftsId) == null)
            {
                _logger.LogWarning("Gift with ID {GiftId} does not exist", bsk.GiftsId);
                throw new KeyNotFoundException("Gift does not exist");
            }

            var createBasket = _mapper.Map<Basket>(bsk);
            var basket = await _basketRepository.CreateBasket(createBasket);

            _logger.LogInformation("Basket created with ID {BasketId}", basket.Id);
            return _mapper.Map<BasketResponseDTO>(basket);
        }

        public async Task<List<BasketResponseDTO>> GetAllBaskets()
        {
            _logger.LogInformation("Retrieving all baskets");
            var ListBaskets = await _basketRepository.GetAllBaskets();
            _logger.LogInformation("Retrieved {Count} baskets", ListBaskets.Count);
            return ListBaskets.Select(b => _mapper.Map<BasketResponseDTO>(b)).ToList();
        }

        public async Task<List<BasketResponseDTO>?> GetBasketById(int id)
        {
            _logger.LogInformation("Retrieving basket by ID {Id}", id);
            var basket = await _basketRepository.GetBasketById(id);

            if (basket == null)
            {
                _logger.LogWarning("No basket found with ID {Id}", id);
                return null;
            }

            return basket.Select(b => _mapper.Map<BasketResponseDTO>(b)).ToList();
        }

        public async Task<bool> DeleteBasket(int id)
        {
            if (!await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to delete basket while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot delete basket.");
            }
            _logger.LogInformation("Deleting basket with ID {Id}", id);
            var result = await _basketRepository.DeleteBasket(id);

            if (result)
                _logger.LogInformation("Basket with ID {Id} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete basket with ID {Id}", id);

            return result;
        }

        public async Task<List<BasketResponseDTO?>> GetPurchasesById(int id)
        {
            _logger.LogInformation("Retrieving basket by ID {Id}", id);
            var basket = await _basketRepository.GetPurchasesById(id);

            if (basket == null)
            {
                _logger.LogWarning("No basket found with ID {Id}", id);
                return null;
            }

            return basket.Select(b => _mapper.Map<BasketResponseDTO>(b)).ToList();
        }

        public async Task<bool> UpdateBasket(BasketUpdateDTO bsk, int id)
        {
            if (!await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to update basket while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot update basket.");
            }
            var updateBasket = new Basket
            {
                Id = id,
                Status = bsk.Status
            };

            return await _basketRepository.UpdateBasket(updateBasket);
        }
        public async Task<bool> DeleteByGift(int giftId, int userId)
        {
            if (!await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to delete basket by gift while sale is closed");
                throw new InvalidOperationException("Sale is currently closed. Cannot delete basket.");
            }
            _logger.LogInformation("Deleting basket with GiftId {GiftId} for UserId {UserId}", giftId, userId);
            var result = await _basketRepository.DeleteByGift(giftId, userId);

            if (result)
                _logger.LogInformation("Basket with GiftId {GiftId} for UserId {UserId} deleted successfully", giftId, userId);
            else
                _logger.LogWarning("Failed to delete basket with GiftId {GiftId} for UserId {UserId}", giftId, userId);

            return result;
        }
    }


    }



