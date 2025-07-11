using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly InMemoryDataService _data;

        public BrandController(InMemoryDataService data)
        {
            _data = data;
        }

        // GET: /api/brand?manufacturerId=5
        [HttpGet]
        public IActionResult GetByManufacturer([FromQuery] int manufacturerId)
        {
            var brands = _data.Brands.Where(b => b.ManufacturerId == manufacturerId).ToList();
            return Ok(brands);
        }
    }
}
