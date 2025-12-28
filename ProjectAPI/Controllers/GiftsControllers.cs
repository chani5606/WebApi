using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectAPI.Services;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsControllers : ControllerBase
    {
        private readonly IGiftsServices _service;

        public GiftsControllers(IGiftsServices service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateGifts")]
        public async Task<ActionResult<GifttResponseDTOs>> CreateGift([FromBody] GiftCreateDTOs gift)
        {
            var res = await _service.CreateGift(gift);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetAllGifts")]
        public async Task<ActionResult<List<GifttResponseDTOs>>> GetAllGifts()
        {
            var res = await _service.GetAllGifts();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Route("GetGiftsByID")]
        public async Task<ActionResult<GifttResponseDTOs>> GetGiftsByID(int id)
        {
            var res = await _service.GetGiftsByID(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Route("UpdateGift")]
        public async Task<ActionResult<GifttResponseDTOs>> UpdateGift([FromBody] GiftUpdateDTOs gift, int id)
        {
            var res = await _service.UpdateGifts(gift,id);
            return Ok(res);
        }

        [HttpDelete]
        [Route("DeleteGift")]
        public async Task<ActionResult<bool>> DeleteGift(int id)
        {
            var res = await _service.DeleteGifts(id);
            return Ok(res);
        }
    }
}
