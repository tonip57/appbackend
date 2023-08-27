using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos;
using WebApp.Services;

namespace WepApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricityPricesController : Controller
    {
        private readonly IElectricityPriceService _electricityPriceService;

        public ElectricityPricesController(IElectricityPriceService electricityPriceService)
        {
            _electricityPriceService = electricityPriceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ElectricityPriceDto>))]
        public async Task<IActionResult> GetElectricityPrices()
        {
            try
            {
                var response = await _electricityPriceService.GetElectricityPrices();
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}