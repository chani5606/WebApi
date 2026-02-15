using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Services;
using ProjectFinal.Dto;
using ProjectFinal.Interfaces;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DonorControllers : ControllerBase
        {
            private readonly IDonorServicecs _service;
            private readonly ILogger<DonorControllers> _logger;

            public DonorControllers(IDonorServicecs service, ILogger<DonorControllers> logger)
            {
                _service = service;
                _logger = logger;
            }

            [Authorize(Roles = "Manager")]
            [HttpPost("Create")]
            public async Task<ActionResult<DonorResponseDTOs>> CreateDonor(DonorCreateDTOs d)
            {
                _logger.LogInformation("Create donor");
                var res = await _service.CreateDonor(d);
                return Ok(res);
            }

            [Authorize]
            [HttpGet("GetAll")]
            public async Task<ActionResult<List<DonorResponseDTOs>>> GetAllDonors()
            {
                _logger.LogInformation("Get all donors");
                var res = await _service.GetAllDonors();
                return Ok(res);
            }

            [Authorize]
            [HttpGet("GetByID/{id}")]
            public async Task<ActionResult<DonorResponseDTOs>> GetDonorByID(int id)
            {
                _logger.LogInformation("Get donor by id {Id}", id);

                var res = await _service.GetDonorById(id);
                if (res == null)
                {
                    _logger.LogWarning("Donor not found. Id={Id}", id);
                    return NotFound();
                }
                return Ok(res);
            }

            [Authorize(Roles = "Manager")]
            [HttpPut("Update/{id}")]
            public async Task<ActionResult<DonorResponseDTOs>> UpdateDonor(DonorUpdateDTOs d, int id)
            {
                _logger.LogInformation("Update donor {Id}", id);
                var res = await _service.UpdateDonor(d, id);
                return Ok(res);
            }

            [Authorize(Roles = "Manager")]
            [HttpDelete("Delete/{id}")]
            public async Task<ActionResult<bool>> DeleteDonor(int id)
            {
                _logger.LogInformation("Delete donor {Id}", id);
                bool res = await _service.DeleteDonor(id);
                return Ok(res);
            }

            [Authorize]
            [HttpGet("FindByGifts/{idGift}")]
            public async Task<ActionResult<DonorResponseDTOs>> FindDonorByGifts(int idGift)
            {
                _logger.LogInformation("Find donor by gift {GiftId}", idGift);

                var res = await _service.FindDonorByGifts(idGift);
                if (res == null)
                {
                    _logger.LogWarning("No donor found for gift {GiftId}", idGift);
                    return NotFound();
                }
                return Ok(res);
            }
        }
    }

