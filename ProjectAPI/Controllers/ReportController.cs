using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;
        private readonly ILogger<ReportController> _logger;
        private readonly LotteryContext _context;

        public ReportController(IReportServices reportServices, ILogger<ReportController> logger,LotteryContext context  )      {
            _reportServices = reportServices;
            _logger = logger;
            this._context = context;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("total-income")]
        public async Task<ActionResult<int>> GetTotalIncome()
        {
            _logger.LogInformation("Get total income report");

            var totalIncome = await _reportServices.GetTotalIncome();
            return Ok(totalIncome);
        }
  

    }

}
