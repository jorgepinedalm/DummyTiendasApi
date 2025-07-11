using DemoTiendasApi.Dtos;
using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(InMemoryDataService data) : ControllerBase
    {
        private readonly InMemoryDataService _data = data;

        [HttpGet]
        public IActionResult Get() =>
            Ok(_data.Products.Where(p => !p.IsDeleted));

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _data.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? ean, [FromQuery] string? name)
        {
            if (string.IsNullOrWhiteSpace(ean) && string.IsNullOrWhiteSpace(name))
                return BadRequest("Debe proporcionar al menos el EAN o el nombre para la búsqueda.");

            var products = _data.Products.Where(p => !p.IsDeleted).ToList();

            // Búsqueda por EAN exacto
            if (!string.IsNullOrWhiteSpace(ean))
            {
                var result = products.Where(p => p.EAN == ean).ToList();
                return Ok(result);
            }

            // Búsqueda aproximada por nombre
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Normalizamos el texto de búsqueda y nombre de productos
                string search = name.Trim().ToLowerInvariant();

                // Puedes ajustar el umbral de similitud para hacer la búsqueda más/menos estricta
                int maxDistance = 3;

                var results = products
                    .Select(p => new
                    {
                        Product = p,
                        Distance = LevenshteinDistance(p.Name.ToLowerInvariant(), search)
                    })
                    .Where(x =>
                        x.Product.Name.ToLowerInvariant().Contains(search) ||
                        x.Distance <= maxDistance
                    )
                    .OrderBy(x => x.Distance)
                    .Select(x => x.Product)
                    .ToList();

                return Ok(results);
            }

            return Ok(new List<Product>());
        }

        [HttpPost]
        [RequestSizeLimit(6_000_000)]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            // Validación de imagen
            string? imageBase64 = null;
            if (dto.Image != null)
            {
                if (dto.Image.Length > 5 * 1024 * 1024)
                    return BadRequest("La imagen no debe superar los 5MB.");

                using var ms = new MemoryStream();
                await dto.Image.CopyToAsync(ms);
                imageBase64 = Convert.ToBase64String(ms.ToArray());
            }

            var newId = _data.Products.Any() ? _data.Products.Max(p => p.Id) + 1 : 1;
            var ean = GenerateUniqueEAN13(_data); // mismo método que ya tienes

            var newProduct = new Product
            {
                Id = newId,
                MeasurementUnitId = dto.MeasurementUnitId,
                PurchaseTaxId = dto.PurchaseTaxId,
                SalesTaxId = dto.SalesTaxId,
                PresentationId = dto.PresentationId,
                EAN = ean,
                Name = dto.Name,
                Description = dto.Description,
                SalesPrice = dto.SalesPrice,
                NetContent = dto.NetContent,
                Manufacturer = dto.Manufacturer,
                Brand = dto.Brand,
                Image = imageBase64,
                IsDeleted = false
            };

            _data.Products.Add(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        [RequestSizeLimit(6_000_000)] // Hasta 6MB por seguridad
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDto dto)
        {
            var product = _data.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return NotFound();

            // EAN no se puede modificar

            product.MeasurementUnitId = dto.MeasurementUnitId;
            product.PurchaseTaxId = dto.PurchaseTaxId;
            product.SalesTaxId = dto.SalesTaxId;
            product.PresentationId = dto.PresentationId;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.SalesPrice = dto.SalesPrice;
            product.NetContent = dto.NetContent;
            product.Manufacturer = dto.Manufacturer;
            product.Brand = dto.Brand;

            // Manejo de imagen (si se envía)
            if (dto.Image != null)
            {
                if (dto.Image.Length > 5 * 1024 * 1024)
                    return BadRequest("La imagen no debe superar los 5MB.");

                using var ms = new MemoryStream();
                await dto.Image.CopyToAsync(ms);
                product.Image = Convert.ToBase64String(ms.ToArray());
            }
            // Si no se envía, se conserva la imagen previa

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _data.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null) return NotFound();

            product.IsDeleted = true;
            return NoContent();
        }

        // --- Método para generar EAN-13 único y válido ---
        private string GenerateUniqueEAN13(InMemoryDataService data)
        {
            string ean;
            Random rnd = new Random();
            do
            {
                // Puedes usar un prefijo fijo para simular un país/empresa, por ejemplo "770" (Colombia)
                var code = "770" + rnd.Next(100000000, 999999999).ToString();
                ean = EAN13WithChecksum(code);
            } while (data.Products.Any(p => p.EAN == ean));
            return ean;
        }

        // Calcula el dígito de control EAN-13
        private string EAN13WithChecksum(string base12)
        {
            if (base12.Length != 12) throw new ArgumentException("EAN base debe tener 12 dígitos");
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(base12[i].ToString());
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            int checksum = (10 - (sum % 10)) % 10;
            return base12 + checksum;
        }

        // Distancia de Levenshtein para similitud de cadenas
        private static int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;

            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[s.Length, t.Length];
        }
    }
}
