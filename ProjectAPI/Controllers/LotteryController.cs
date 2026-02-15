
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotteryController : ControllerBase
    {
        private readonly IWinnerServises winnerServises;
        private readonly IGiftRepository giftRepository;
        private readonly IReportServices reportServices;
        private readonly ILogger<LotteryController> _logger;
        private readonly ISaleReapository _saleReapository;

        public LotteryController(
            IWinnerServises winnerServises,
            IGiftRepository giftRepository,
            IReportServices reportServices,
            ILogger<LotteryController> logger,
            ISaleReapository saleReapository
            )
        {
            this.winnerServises = winnerServises;
            this.giftRepository = giftRepository;
            this.reportServices = reportServices;
            _logger = logger;
            _saleReapository = saleReapository;
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("draw-winner")]
        public async Task<ActionResult<Winner>> DrawWinner([FromBody] int GiftId)
        {
            if  ( await _saleReapository.IsSaleOpen())
            {
                _logger.LogWarning("Attempt to draw winner while sale is open. GiftId={GiftId}", GiftId);
                return BadRequest("לא ניתן לבחור זוכה בזמן שהמבצע פתוח");
            }
            _logger.LogInformation("Draw winner for GiftId={GiftId}", GiftId);

            var winner = await giftRepository.GetGiftsByID(GiftId);
            if (winner == null)
            {
                _logger.LogWarning("Gift not found. GiftId={GiftId}", GiftId);
                return BadRequest("Gift not found");
            }

            if (await winnerServises.GetById(GiftId) != null)
            {
                _logger.LogWarning("Winner already exists for GiftId={GiftId}", GiftId);
                return BadRequest("כבר נבחר זוכה למתנה זו");
            }

            var random = new Random();
            int index = random.Next(winner.baskets.Count);
            var selectedWinner = winner.baskets[index];

            var createWinner = await winnerServises.Create(new WinnerCreatedDTO
            {
                UserId = selectedWinner.UserId,
                GiftId = GiftId,
            });

            _logger.LogInformation("Winner selected. UserId={UserId}, GiftId={GiftId}",
                selectedWinner.UserId, GiftId);

            await reportServices.SendWinnerEmail(
                selectedWinner.User.Email,
                selectedWinner.User.FirstName,
                selectedWinner.Gifts.Name);

            return Ok(createWinner);
        }
    }




}

