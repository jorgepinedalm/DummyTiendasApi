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
                new() { Id = 2, Code = "CE", Description = "Cédula de extranjeria" },
                new() { Id = 3, Code = "PA", Description = "Pasaporte" },
                new() { Id = 4, Code = "NIT", Description = "NIT" }
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
                new() { Id = 1, Name = "Pago de contado" },
                new() { Id = 2, Name = "Pago de crédito" },
            };

                // Presentations
                Presentations = new List<Presentation>
            {
                new() { Id = 1, Name = "Caja" },
                new() { Id = 2, Name = "Paquete" },
                new() { Id = 3, Name = "Unidad" }
            };

                // Measurement Units
                MeasurementUnits = new List<MeasurementUnit>
            {
                new() { Id = 1, Name = "Unidad" },
                new() { Id = 2, Name = "Litro" },
                new() { Id = 3, Name = "Mililitro" },
                new() { Id = 4, Name = "Gramo" },
                new() { Id = 5, Name = "Kilogramo" },
                new() { Id = 6, Name = "Metro" },
                new() { Id = 7, Name = "Centimetro" }
            };

                // Taxes
                PurchaseTaxes = new List<PurchaseTax>
            {
                new() { Id = 1, Name = "Exento", Value = 0 },
                new() { Id = 2, Name = "Excluido", Value = 0 },
                new() { Id = 3, Name = "IVA 5%", Value = 0.05m },
                new() { Id = 4, Name = "IVA 8%", Value = 0.08m },
                new() { Id = 5, Name = "IVA 16%", Value = 0.16m },
                new() { Id = 6, Name = "IVA 19%", Value = 0.19m },
            };

                SalesTaxes = new List<SalesTax>
            {
                new() { Id = 1, Name = "Exento", Value = 0 },
                new() { Id = 2, Name = "Excluido", Value = 0 },
                new() { Id = 3, Name = "IVA 5%", Value = 0.05m },
                new() { Id = 4, Name = "IVA 8%", Value = 0.08m },
                new() { Id = 5, Name = "IVA 16%", Value = 0.16m },
                new() { Id = 6, Name = "IVA 19%", Value = 0.19m },
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
