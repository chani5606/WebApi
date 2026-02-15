using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleServices _saleServices;
        public SaleController(ISaleServices saleServices)
        {
            _saleServices = saleServices;
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("create")]
        public async Task<ActionResult<Sale>> CreateSale(Sale sale)
        {
            await _saleServices.CreateSale(sale);
            return Ok(sale);
        }
        //[Authorize(Roles = "Manager")]
        [HttpGet("get-all")]
        public async Task<ActionResult<Sale>> GetSales()
        {
            var sales = await _saleServices.GetSales();
            return Ok(sales);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Sale>> UpdateSale(Sale sale)
        {
          var s =  await _saleServices.UpdateSale(sale);
            return Ok(s);
        }
        //[Authorize(Roles = "Manager")]
        [HttpGet("IsOpen")]
        public async Task<ActionResult<bool>> IsOpen()
        {
           
            return Ok(await _saleServices.IsSaleOpen());

        }
        //[Authorize(Roles = "manager")]
        [HttpDelete("reset")]
        public async Task<ActionResult<bool>> ResetSale()
        {
            bool res = await _saleServices.resertSale();
            if (res)
                return Ok(res);
            else
                return BadRequest("שגיאה באיפוס המכירה");
        }

    }
}
