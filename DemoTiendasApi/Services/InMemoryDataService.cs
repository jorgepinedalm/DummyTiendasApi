using DemoTiendasApi.Models;

namespace DemoTiendasApi.Services
{
    public class InMemoryDataService
    {
        public List<Product> Products { get; set; } = new();
        public List<Purchase> Purchases { get; set; } = new();
        public List<Presentation> Presentations { get; set; } = new();
        public List<MeasurementUnit> MeasurementUnits { get; set; } = new();
        public List<PurchaseTax> PurchaseTaxes { get; set; } = new();
        public List<SalesTax> SalesTaxes { get; set; } = new();
        public List<Supplier> Suppliers { get; set; } = new();
        public List<DocType> DocTypes { get; set; } = new();
        public List<Warehouse> Warehouses { get; set; } = new();
        public List<PaymentMethod> PaymentMethods { get; set; } = new();

        public InMemoryDataService()
        {
            // Doc Types
            DocTypes = new List<DocType>
            {
                new() { Id = 1, Code = "CC", Description = "Cédula de ciudadanía" },
                new() { Id = 2, Code = "NIT", Description = "NIT" }
            };

                // Suppliers
                Suppliers = new List<Supplier>
            {
                new() { Id = 1, Name = "Proveedor Uno", DocTypeId = 2, Identification = 900123456, Address = "Calle 1 #12-34", Phone = "3001234567", Email = "proveedor1@correo.com" },
                new() { Id = 2, Name = "Proveedor Dos", DocTypeId = 2, Identification = 900987654, Address = "Carrera 7 #56-78", Phone = "3019876543", Email = "proveedor2@correo.com" }
            };

                // Warehouses
                Warehouses = new List<Warehouse>
            {
                new() { Id = 1, Name = "Bodega Principal" },
                new() { Id = 2, Name = "Bodega Secundaria" }
            };

                // Payment Methods
                PaymentMethods = new List<PaymentMethod>
            {
                new() { Id = 1, Name = "Efectivo" },
                new() { Id = 2, Name = "Transferencia" },
                new() { Id = 3, Name = "Tarjeta" }
            };

                // Presentations
                Presentations = new List<Presentation>
            {
                new() { Id = 1, Name = "Caja" },
                new() { Id = 2, Name = "Botella" }
            };

                // Measurement Units
                MeasurementUnits = new List<MeasurementUnit>
            {
                new() { Id = 1, Name = "Unidad" },
                new() { Id = 2, Name = "Litro" },
                new() { Id = 3, Name = "Kilogramo" }
            };

                // Taxes
                PurchaseTaxes = new List<PurchaseTax>
            {
                new() { Id = 1, Name = "IVA 19%", Value = 0.19m },
                new() { Id = 2, Name = "IVA 5%", Value = 0.05m }
            };

                SalesTaxes = new List<SalesTax>
            {
                new() { Id = 1, Name = "IVA 19%", Value = 0.19m },
                new() { Id = 2, Name = "IVA 5%", Value = 0.05m }
            };

                // Products
                Products = new List<Product>
            {
                new() {
                    Id = 1,
                    MeasurementUnitId = 1,
                    PurchaseTaxId = 1,
                    SalesTaxId = 1,
                    PresentationId = 1,
                    EAN = "7701234567890",
                    Name = "Leche Entera",
                    Description = "Leche entera en caja",
                    SalesPrice = 3000,
                    NetContent = 1,
                    Manufacturer = "Colanta",
                    Brand = "Colanta",
                    Image = null
                },
                new() {
                    Id = 2,
                    MeasurementUnitId = 2,
                    PurchaseTaxId = 2,
                    SalesTaxId = 2,
                    PresentationId = 2,
                    EAN = "7709876543210",
                    Name = "Aceite Vegetal",
                    Description = "Aceite vegetal en botella",
                    SalesPrice = 12000,
                    NetContent = 2,
                    Manufacturer = "Oliosa",
                    Brand = "Oliosa",
                    Image = null
                }
            };

                // Purchases de ejemplo
                Purchases = new List<Purchase>
            {
                new() {
                    Id = 1,
                    SupplierId = 1,
                    WarehouseId = 1,
                    PaymentMethodId = 1,
                    InvoiceNumber = "FV-001",
                    Date = DateTime.UtcNow.AddDays(-2),
                    Products = new List<PurchaseProduct>
                    {
                        new() { ProductId = 1, Quantity = 10 },
                        new() { ProductId = 2, Quantity = 5 }
                    }
                }
            };
        }
    }
}
