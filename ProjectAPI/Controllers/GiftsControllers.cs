using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectAPI.Services;

namespace ProjectAPI.Controllers
{    
    [ApiController]

    [Route("api/[controller]")]
    public class GiftsControllers : ControllerBase
    {
        private readonly IGiftsServices _service;
        private readonly ILogger<GiftsControllers> _logger;

        public GiftsControllers(IGiftsServices service, ILogger<GiftsControllers> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateGifts")]
        public async Task<ActionResult<GifttResponseDTOs>> CreateGift([FromBody]GiftCreateDTOs gift)
        {
            _logger.LogInformation("Create gift");

            try
            {
                var res = await _service.CreateGift(gift);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create gift failed");
                return BadRequest(new { message = ex.Message });
            }
        }

        //[Authorize]
        [HttpGet("GetAllGifts")]
        public async Task<ActionResult<List<GifttResponseDTOs>>> GetAllGifts()
        {
            _logger.LogInformation("Get all gifts");
            var res = await _service.GetAllGifts();
            return Ok(res);
        }

        [Authorize]
        [HttpGet("GetGiftsByID/{id}")]
        public async Task<ActionResult<GifttResponseDTOs>> GetGiftsByID(int id)
        {
            _logger.LogInformation("Get gift by id {Id}", id);

            var res = await _service.GetGiftsByID(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        //[Authorize(Roles = "Manager")]
        [HttpPut("UpdateGift/{id}")]
        public async Task<ActionResult<GifttResponseDTOs>> UpdateGift([FromBody] GiftUpdateDTOs gift, int id)
        {
            _logger.LogInformation("Update gift {Id}", id);

            try
            {
                var res = await _service.UpdateGifts(gift, id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Update gift failed. Id={Id}", id);
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("DeleteGift/{id}")]
        //שיניתי
        public async Task<ActionResult<bool>> DeleteGift(int id)
        {
            _logger.LogInformation("Delete gift {Id}", id);
            var res = await _service.DeleteGifts(id);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("FindGiftByname")]
        public async Task<ActionResult<GifttResponseDTOs>> FindGiftByname([FromQuery] string name)
        {
            _logger.LogInformation("Find gift by name {Name}", name);
            var res = await _service.FindGiftByname(name);
            return Ok(res);
        }
        //[Authorize]
        [HttpGet("FindGiftByDonor")]
        public async Task<ActionResult<List<GifttResponseDTOs>>> FindGiftByDonor([FromQuery] string d)
        {
            _logger.LogInformation("Find gifts by donor {Donor}", d);
            var res = await _service.FindGiftByDonor(d);
            return Ok(res);
        }
    }

}

