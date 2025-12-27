using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Services;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorControllers : ControllerBase
    {
        private readonly DonorServices _service = new();

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGift([FromBody] Donors d)
        {
            bool res = await _service.CreateDonor(d);
            if (!res)
            {
                return BadRequest("the donors is not exisit");
            }
            return Ok(d);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllDonors()
        {
            var res = await _service.GetAllDonors();
            return Ok(res);
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetDonorByID(int id)
        {
            var res = await _service.GetDonorByID(id);
            return Ok(res);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateGift([FromBody] Donors d, int id)
        {
            var res = await _service.UpdateDonor(d, id);
            return Ok(res);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteGift(int id)
        {
            var res = await _service.DeleteDonor(id);
            return Ok(res);
        }
    }
}
