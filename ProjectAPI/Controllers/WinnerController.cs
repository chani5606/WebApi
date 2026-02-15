using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerServises _services;
        private readonly ILogger<WinnerController> _logger;

        public WinnerController(IWinnerServises services, ILogger<WinnerController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<WinnerResponseDTO>> GetAll()
        {
            _logger.LogInformation("Get all winners");
            var winner = await _services.GetAll();
            return Ok(winner);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<WinnerResponseDTO>> GetById(int id)
        {
            _logger.LogInformation("Get winner by id {Id}", id);

            var winner = await _services.GetById(id);
            if (winner == null)
            {
                _logger.LogWarning("Winner not found. Id={Id}", id);
                return NotFound();
            }
            return Ok(winner);
        }
    }


}

