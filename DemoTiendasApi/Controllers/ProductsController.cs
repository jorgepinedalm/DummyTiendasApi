using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly InMemoryDataService _data;

        public ProductsController(InMemoryDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_data.Products);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _data.Products.FirstOrDefault(x => x.Id == id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            product.Id = _data.Products.Any() ? _data.Products.Max(x => x.Id) + 1 : 1;
            _data.Products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            var existing = _data.Products.FirstOrDefault(x => x.Id == id);
            if (existing is null) return NotFound();
            existing.Name = product.Name;
            existing.SalesPrice = product.SalesPrice;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _data.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();
            _data.Products.Remove(product);
            return NoContent();
        }
    }
}
