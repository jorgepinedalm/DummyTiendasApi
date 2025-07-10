using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly InMemoryDataService _data;

        public PurchasesController(InMemoryDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_data.Purchases);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var purchase = _data.Purchases.FirstOrDefault(x => x.Id == id);
            return purchase is null ? NotFound() : Ok(purchase);
        }

        [HttpPost]
        public IActionResult Post(Purchase purchase)
        {
            purchase.Id = _data.Purchases.Any() ? _data.Purchases.Max(x => x.Id) + 1 : 1;
            purchase.Date = DateTime.UtcNow;
            _data.Purchases.Add(purchase);
            return CreatedAtAction(nameof(Get), new { id = purchase.Id }, purchase);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var purchase = _data.Purchases.FirstOrDefault(x => x.Id == id);
            if (purchase is null) return NotFound();
            _data.Purchases.Remove(purchase);
            return NoContent();
        }
    }
}
