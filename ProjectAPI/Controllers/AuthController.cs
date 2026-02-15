using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ProjectAPI.DTOs;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly ILogger<AuthController> logger;

        public AuthController(IUserServices userServices, ILogger<AuthController> logger)
        {
            this.userServices = userServices;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            logger.LogInformation("Login attempt for email: {Email}", loginDTO?.Email);

            if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                logger.LogWarning("Login failed – missing email or password");
                return BadRequest("Email and Password are required.");
            }

            var result = await userServices.Authenticate(loginDTO.Email, loginDTO.Password);

            if (result == null)
            {
                logger.LogWarning("Login failed – invalid credentials for email: {Email}", loginDTO.Email);
                return Unauthorized("Invalid email or password.");
            }

            logger.LogInformation("Login successful for email: {Email}", loginDTO.Email);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserCreateDTO userCreateDTO)
        {
            logger.LogInformation("Register attempt for email: {Email}", userCreateDTO?.Email);

            try
            {
                var result = await userServices.CreateUser(userCreateDTO);

                logger.LogInformation("User registered successfully. UserId={UserId}, Email={Email}",
                    result?.Id, userCreateDTO.Email);
                return Ok(new
                {
                    message = "User registered successfully."
                });


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while registering user. Email={Email}", userCreateDTO?.Email);
                return BadRequest(ex.Message);
            }
        }
    }
}
