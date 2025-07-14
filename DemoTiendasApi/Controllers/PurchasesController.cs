using DemoTiendasApi.Dtos;
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
        public IActionResult Get(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] int? supplierId = null,
    [FromQuery] int? warehouseId = null,
    [FromQuery] DateTime? from = null,
    [FromQuery] DateTime? to = null)
        {
            var query = _data.Purchases.AsQueryable();

            if (supplierId.HasValue)
                query = query.Where(x => x.SupplierId == supplierId.Value);

            if (warehouseId.HasValue)
                query = query.Where(x => x.WarehouseId == warehouseId.Value);

            if (from.HasValue)
                query = query.Where(x => x.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.Date <= to.Value);

            var totalCount = query.Count();

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var purchases = query
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Proyección enriquecida
            var result = purchases.Select(p => new PurchaseDto
            {
                Id = p.Id,
                Warehouse = _data.Warehouses.FirstOrDefault(w => w.Id == p.WarehouseId),
                Supplier = _data.Suppliers.FirstOrDefault(s => s.Id == p.SupplierId),
                PaymentMethod = _data.PaymentMethods.FirstOrDefault(pm => pm.Id == p.PaymentMethodId),
                InvoiceNumber = p.InvoiceNumber,
                Date = p.Date,
                CashAmount = p.CashAmount,
                TransferAmount = p.TransferAmount,
                CardAmount = p.CardAmount,
                Products = [.. p.Products.Select(pp => new PurchaseProductDto
                {
                    Product = _data.Products.FirstOrDefault(prod => prod.Id == pp.ProductId),
                    Quantity = pp.Quantity,
                    UnitPrice = pp.SalePrice
                })]
            }).ToList();

            return Ok(new
            {
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                items = result
            });
        }

        [HttpGet("summary")]
        public IActionResult GetSummary(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? supplierId = null,
            [FromQuery] int? warehouseId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var query = _data.Purchases.AsQueryable();

            var totalCount = query.Count();

            if (supplierId.HasValue)
                query = query.Where(x => x.SupplierId == supplierId.Value);

            if (warehouseId.HasValue)
                query = query.Where(x => x.WarehouseId == warehouseId.Value);

            if (from.HasValue)
                query = query.Where(x => x.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.Date <= to.Value);

            
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var purchases = query
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var results = purchases.Select(p =>
            {
                // Subtotal: suma de (unitPrice * quantity) de los productos de la compra
                decimal subtotal = p.Products.Sum(pp => pp.CostWithTax * pp.Quantity);
                decimal total = subtotal * 1.19M; // 19% cargo

                return new PurchaseSummaryDto
                {
                    Id = p.Id,
                    WarehouseName = _data.Warehouses.FirstOrDefault(w => w.Id == p.WarehouseId)?.Name ?? "",
                    InvoiceNumber = p.InvoiceNumber,
                    SupplierName = _data.Suppliers.FirstOrDefault(s => s.Id == p.SupplierId)?.Name ?? "",
                    Date = p.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    Charges = "19%",
                    Subtotal = subtotal,
                    Total = total
                };
            }).ToList();

            return Ok(new
            {
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                items = results
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var purchase = _data.Purchases.FirstOrDefault(p => p.Id == id);
            if (purchase == null) return NotFound();
            return Ok(purchase);
        }

        [HttpPost]
        public IActionResult Create(CreatePurchaseDto purchase)
        {
            var newId = _data.Purchases.Any() ? _data.Purchases.Max(p => p.Id) + 1 : 1;

            // Validar que todos los productos existan y NO estén eliminados
            foreach (var item in purchase.Products)
            {
                var prod = _data.Products.FirstOrDefault(p => p.Id == item.ProductId && !p.IsDeleted);
                if (prod == null)
                    return BadRequest($"El producto con id {item.ProductId} no existe o está eliminado.");

                if (item.TotalPrice <= 0)
                    return BadRequest($"El precio unitario debe ser mayor que cero para el producto {item.ProductId}.");
                if (item.Quantity <= 0)
                    return BadRequest($"La cantidad debe ser mayor que cero para el producto {item.ProductId}.");
            }

            // Calcula el total ingresado para los productos de la compra
            decimal total = purchase.Products.Sum(x => x.CostWithTax * x.Quantity);

            if (purchase.PaymentMethodId == 1)
            {
                if (purchase.CashAmount == null || purchase.TransferAmount == null || purchase.CardAmount == null)
                    return BadRequest("Debe indicar los montos de efectivo, transferencia y datáfono para la forma de pago combinada.");

                var sum = purchase.CashAmount.Value + purchase.TransferAmount.Value + purchase.CardAmount.Value;
                if (sum != total)
                    return BadRequest($"La suma de efectivo, transferencia y datáfono ({sum}) debe ser igual al total de la compra ({total}).");
            }

            var newPurchase = new Purchase
            {
                Id = newId,
                SupplierId = purchase.SupplierId,
                WarehouseId = purchase.WarehouseId,
                PaymentMethodId = purchase.PaymentMethodId,
                InvoiceNumber = purchase.InvoiceNumber,
                Date = DateTime.UtcNow,
                Products = purchase.Products,
                CashAmount = purchase.CashAmount,
                TransferAmount = purchase.TransferAmount,
                CardAmount = purchase.CardAmount
            };

            _data.Purchases.Add(newPurchase);
            return CreatedAtAction(nameof(GetById), new { id = newPurchase.Id }, newPurchase);
        }
    }
}
