using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController(InMemoryDataService data) : ControllerBase
    {
        private readonly InMemoryDataService _data = data;

        [HttpGet]
        public IActionResult Get() => Ok(_data.Presentations);
    }
}
