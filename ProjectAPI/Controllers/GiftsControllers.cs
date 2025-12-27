using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Services;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsControllers : ControllerBase
    {
        private readonly GiftsService _service = new();

        [HttpPost]
        [Route("CreateGifts")]
        public async Task<IActionResult> CreateGift([FromBody] GiftCreateDto gift)
        {
            var res = await _service.CreateGift(gift);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetAllGifts")]
        public async Task<IActionResult> GetAllGifts()
        {
            var res = await _service.GetAllGifts();
            return Ok(res);
        }

        [HttpGet]
        [Route("GetGiftsByID")]
        public async Task<IActionResult> GetGiftsByID(int id)
        {
            var res = await _service.GetGiftsByID(id);
            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateGift")]
        public async Task<IActionResult> UpdateGift([FromBody] GiftCreateDto gift, int id)
        {
            var res = await _service.UpdateGifts(gift, id);
            return Ok(res);
        }

        [HttpDelete]
        [Route("DeleteGift")]
        public async Task<IActionResult> DeleteGift(int id)
        {
            var res = await _service.DeleteGifts(id);
            return Ok(res);
        }
    }
}
