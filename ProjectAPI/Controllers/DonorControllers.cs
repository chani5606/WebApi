using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Services;
using ProjectFinal.Dto;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorControllers : ControllerBase
    {
        private readonly DonorServices _service;
        public DonorControllers(DonorServices service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<DonorResponseDTOs>> CreateGift([FromBody] DonorCreateDTOs d)
        {
            var res = await _service.CreateDonor(d);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<DonorResponseDTOs>>> GetAllDonors()
        {
            var res = await _service.GetAllDonors();
            return Ok(res);
        }

        [HttpGet ("GetByID/{id}")]
        public async Task<ActionResult<DonorResponseDTOs>> GetDonorByID(int id)
        {
            var res = await _service.GetDonorById(id);
            return Ok(res);
        }

        [HttpPut("Update{id}")]
        public async Task<IActionResult> UpdateGift([FromBody] DonorUpdateDTOs d, int id)
        {
            var res = await _service.UpdateDonor(d,id);
            return Ok(res);
        }

        [HttpDelete( "Delete{id}")]
        public async Task<ActionResult<bool>> DeleteGift(int id)
        {
            bool res = await _service.DeleteDonor(id);
            return Ok(res);
        }

        [HttpGet("FindByGifts/{idGift}")]
        public async Task<ActionResult<DonorResponseDTOs>> FindDonorByGifts(int idGift)
        {
            var res = await _service.FindDonorByGifts(idGift);
            return Ok(res);
        }


    }
}
