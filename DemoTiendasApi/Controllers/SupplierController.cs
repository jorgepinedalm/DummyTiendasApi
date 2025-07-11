using DemoTiendasApi.Dtos;
using DemoTiendasApi.Models;
using DemoTiendasApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoTiendasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController(InMemoryDataService data) : ControllerBase
    {
        private readonly InMemoryDataService _data = data;

        [HttpGet]
        public IActionResult Get() => Ok(_data.Suppliers);

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(Array.Empty<Supplier>());

            var suppliers = _data.Suppliers.AsQueryable();

            // Buscar por Identification (como string, sin ceros a la izquierda)
            var result = suppliers.Where(s =>
                (!string.IsNullOrEmpty(s.Identification.ToString()) && s.Identification.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrWhiteSpace(s.Name) && s.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
            );

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplierDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre del proveedor es obligatorio.");
            if (dto.DocTypeId <= 0)
                return BadRequest("Debe indicar un tipo de documento válido.");
            if (string.IsNullOrWhiteSpace(dto.Identification))
                return BadRequest("Debe indicar un número de identificación válido.");

            // Validación adicional: evitar duplicados por identificación
            if (_data.Suppliers.Any(s => s.Identification == dto.Identification))
                return Conflict("Ya existe un proveedor con esa identificación.");

            var newId = _data.Suppliers.Count != 0 ? _data.Suppliers.Max(s => s.Id) + 1 : 1;

            var supplier = new Supplier
            {
                Id = newId,
                Name = dto.Name,
                DocTypeId = dto.DocTypeId,
                Identification = dto.Identification,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email
            };

            _data.Suppliers.Add(supplier);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }

        // (Si no existe GetById, agrega este método básico:)
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var supplier = _data.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SupplierDto dto)
        {
            var supplier = _data.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre del proveedor es obligatorio.");
            if (dto.DocTypeId <= 0)
                return BadRequest("Debe indicar un tipo de documento válido.");
            if (string.IsNullOrWhiteSpace(dto.Identification))
                return BadRequest("Debe indicar un número de identificación válido.");

            // Validación adicional: evitar duplicados por identificación (salvo el mismo proveedor)
            if (_data.Suppliers.Any(s => s.Identification == dto.Identification && s.Id != id))
                return Conflict("Ya existe otro proveedor con esa identificación.");

            supplier.Name = dto.Name;
            supplier.DocTypeId = dto.DocTypeId;
            supplier.Identification = dto.Identification;
            supplier.Address = dto.Address;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;

            return NoContent();
        }
    }

}
