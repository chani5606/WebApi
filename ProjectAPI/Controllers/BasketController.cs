using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _services;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketServices services, ILogger<BasketController> logger)
        {
            _services = services;
            _logger = logger;
        }




        //[Authorize(Roles = "Manager")]
        [HttpGet("GetAllBaskets")]
        public async Task<ActionResult<BasketResponseDTO>> GetAllBaskets()
        {
            _logger.LogInformation("GetAllBaskets requested by Manager");

            var baskets = await _services.GetAllBaskets();

            _logger.LogInformation("GetAllBaskets returned {Count} baskets",
                baskets?.Count() ?? 0);

            return Ok(baskets);
        }

        //[Authorize]
        [HttpGet("GetBasketById/{id}")]
        public async Task<ActionResult<List<BasketResponseDTO>>> GetBasketById(int id)
        {
            _logger.LogInformation("GetBasketById called. BasketId={BasketId}", id);

            var basket = await _services.GetBasketById(id);

            if (basket == null)
            {
                _logger.LogWarning("Basket not found. BasketId={BasketId}", id);
                return NotFound();
            }

            _logger.LogInformation("Basket found. BasketId={BasketId}", id);
            return Ok(basket);
        }

        [Authorize]
        [HttpGet("GetPurchasesById/{id}")]
        public async Task<ActionResult<List<BasketResponseDTO>>> GetPurchasesById(int id)
        {
            _logger.LogInformation("GetPurchasesById called. BasketId={BasketId}", id);

            var basket = await _services.GetPurchasesById(id);

            if (basket == null)
            {
                _logger.LogWarning("Basket not found. BasketId={BasketId}", id);
                return NotFound();
            }

            _logger.LogInformation("Basket found. BasketId={BasketId}", id);
            return Ok(basket);
        }
        [Authorize]
        [HttpPost("CreateBasket")]
        public async Task<ActionResult<BasketResponseDTO>> CreateBasket(BasketCreateDTO bsk)
        {
            _logger.LogInformation("CreateBasket called. GiftId={GiftId}, UserId={UserId}",
                bsk?.GiftsId, bsk?.UserId);

            try
            {
                var createdBasket = await _services.CreateBasket(bsk);

                _logger.LogInformation("Basket created successfully. BasketId={BasketId}",
                    createdBasket?.Id);

                return Ok(createdBasket);
            }
            catch (KeyNotFoundException knf)
            {
                _logger.LogWarning(knf,
                    "CreateBasket failed – related entity not found. GiftId={GiftId}, UserId={UserId}",
                    bsk?.GiftsId, bsk?.UserId);

                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating basket");
                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteBasket/{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(int id)
        {
            _logger.LogInformation("DeleteBasket called. BasketId={BasketId}", id);

            var result = await _services.DeleteBasket(id);

            if (!result)
            {
                _logger.LogWarning("DeleteBasket failed – basket not found. BasketId={BasketId}", id);
                return NotFound();
            }

            _logger.LogInformation("Basket deleted successfully. BasketId={BasketId}", id);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("UpdateBasket/{id}")]
        public async Task<ActionResult<bool>> UpdateBasket(BasketUpdateDTO bsk, int id)
        {
            _logger.LogInformation("UpdateBasket called. BasketId={BasketId}", id);

            var result = await _services.UpdateBasket(bsk, id);

            if (!result)
            {
                _logger.LogWarning("UpdateBasket failed – basket not found. BasketId={BasketId}", id);
                return NotFound();
            }

            _logger.LogInformation("Basket updated successfully. BasketId={BasketId}", id);

            return Ok(result);
        }
        [HttpDelete("deleteByGift/{userId}/{giftId}")]
        public async Task<ActionResult<bool>> DeleteByGift(int userId, int giftId)
        {
            _logger.LogInformation("DeleteByGift called. UserId={UserId}, GiftId={GiftId}", userId, giftId);

           try
            {
                var result = await _services.DeleteByGift(giftId, userId);

                if (!result)
                {
                    _logger.LogWarning("DeleteByGift failed – no matching basket found. UserId={UserId}, GiftId={GiftId}", userId, giftId);
                    return NotFound();
                }

                _logger.LogInformation("Basket deleted successfully by gift. UserId={UserId}, GiftId={GiftId}", userId, giftId);
                return Ok(result);
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogWarning(ioe, "DeleteByGift failed – sale is closed. UserId={UserId}, GiftId={GiftId}", userId, giftId);
                return BadRequest(ioe.Message);
            }


        }
    }
}
