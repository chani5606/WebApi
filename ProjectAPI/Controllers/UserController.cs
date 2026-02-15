using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;
using ProjectFinal.Dto;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserServices userServices, ILogger<UserController> logger)
        {
            _service = userServices;
            _logger = logger;
        }

        //[Authorize(Roles = "Manager")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers()
        {
            _logger.LogInformation("Get all users");
            var res = await _service.GetAllUsers();
            return Ok(res);
        }

        [Authorize]
        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserByID(int id)
        {
            _logger.LogInformation("Get user by id {Id}", id);

            var res = await _service.GetUserById(id);
            if (res == null)
            {
                _logger.LogWarning("User not found. Id={Id}", id);
                return NotFound();
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPut("Update{id}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser(UserUpdateDTO u, int id)
        {
            _logger.LogInformation("Update user {Id}", id);

            try
            {
                var res = await _service.UpdateUser(u, id);
                return Ok(res);
            }
            catch (ArgumentException e)
            {
                _logger.LogWarning(e, "Update user failed. Id={Id}", id);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            _logger.LogInformation("Delete user {Id}", id);
            bool res = await _service.DeleteUser(id);
            return Ok(res);
        }
    }
}

