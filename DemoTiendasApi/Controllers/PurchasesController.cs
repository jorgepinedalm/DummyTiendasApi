using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController(InMemoryDataService data) : ControllerBase
    {
        private readonly InMemoryDataService _data = data;

        [HttpGet]
        public IActionResult Get() => Ok(_data.Purchases);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var purchase = _data.Purchases.FirstOrDefault(p => p.Id == id);
            if (purchase == null) return NotFound();
            return Ok(purchase);
        }

        [HttpPost]
        public IActionResult Create(Purchase purchase)
        {
            var newId = _data.Purchases.Any() ? _data.Purchases.Max(p => p.Id) + 1 : 1;

            var newPurchase = new Purchase
            {
                Id = newId,
                SupplierId = purchase.SupplierId,
                WarehouseId = purchase.WarehouseId,
                PaymentMethodId = purchase.PaymentMethodId,
                InvoiceNumber = purchase.InvoiceNumber,
                Date = DateTime.UtcNow,
                Products = purchase.Products
            };

            _data.Purchases.Add(newPurchase);
            return CreatedAtAction(nameof(GetById), new { id = newPurchase.Id }, newPurchase);
        }
    }
}
