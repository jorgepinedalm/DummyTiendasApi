using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController(InMemoryDataService data) : ControllerBase
    {
        private readonly InMemoryDataService _data = data;

        // GET: /api/manufacturer
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_data.Manufacturers);
        }

        // GET: /api/manufacturer/search?query=texto
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(Array.Empty<Manufacturer>());

            var result = _data.Manufacturers
                .Where(m => m.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }
    }
}
