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
            DocTypes =
            [
                new() { Id = 1, Code = "CC", Description = "Cédula de ciudadanía" },
                new() { Id = 2, Code = "CE", Description = "Cédula de extranjeria" },
                new() { Id = 3, Code = "PA", Description = "Pasaporte" },
                new() { Id = 4, Code = "NIT", Description = "NIT" }
            ];

                // Suppliers
                Suppliers =
            [
                new() { Id = 1, Name = "Proveedor Uno", DocTypeId = 1, Identification = "900123456", Address = "Calle 1 #12-34", Phone = "3001234567", Email = "proveedor1@correo.com" },
                new() { Id = 2, Name = "Proveedor Dos", DocTypeId = 4, Identification = "900987654", Address = "Carrera 7 #56-78", Phone = "3019876543", Email = "proveedor2@correo.com" }
            ];

                // Warehouses
                Warehouses =
            [
                new() { Id = 1, Name = "Bodega Principal" },
                new() { Id = 2, Name = "Bodega Secundaria" }
            ];

                // Payment Methods
                PaymentMethods =
            [
                new() { Id = 1, Name = "Pago de contado" },
                new() { Id = 2, Name = "Pago de crédito" },
            ];

                // Presentations
                Presentations =
            [
                new() { Id = 1, Name = "Caja" },
                new() { Id = 2, Name = "Paquete" },
                new() { Id = 3, Name = "Unidad" }
            ];

                // Measurement Units
                MeasurementUnits =
            [
                new() { Id = 1, Name = "Unidad" },
                new() { Id = 2, Name = "Litro" },
                new() { Id = 3, Name = "Mililitro" },
                new() { Id = 4, Name = "Gramo" },
                new() { Id = 5, Name = "Kilogramo" },
                new() { Id = 6, Name = "Metro" },
                new() { Id = 7, Name = "Centimetro" }
            ];

                // Taxes
                PurchaseTaxes =
            [
                new() { Id = 1, Name = "Exento", Value = 0 },
                new() { Id = 2, Name = "Excluido", Value = 0 },
                new() { Id = 3, Name = "IVA 5%", Value = 0.05m },
                new() { Id = 4, Name = "IVA 8%", Value = 0.08m },
                new() { Id = 5, Name = "IVA 16%", Value = 0.16m },
                new() { Id = 6, Name = "IVA 19%", Value = 0.19m },
            ];

                SalesTaxes =
            [
                new() { Id = 1, Name = "Exento", Value = 0 },
                new() { Id = 2, Name = "Excluido", Value = 0 },
                new() { Id = 3, Name = "IVA 5%", Value = 0.05m },
                new() { Id = 4, Name = "IVA 8%", Value = 0.08m },
                new() { Id = 5, Name = "IVA 16%", Value = 0.16m },
                new() { Id = 6, Name = "IVA 19%", Value = 0.19m },
            ];

                // Products
                Products =
            [
                new() {
                    Id = 1,
                    MeasurementUnitId = 1,
                    PurchaseTaxId = 1,
                    SalesTaxId = 6,
                    PresentationId = 1,
                    EAN = "012345678905",
                    Name = "Leche Entera",
                    Description = "Leche entera en caja",
                    SalesPrice = 3000,
                    NetContent = 1,
                    Manufacturer = "Colanta",
                    Brand = "Colanta",
                    Image = null,
                    Quantity = 5
                },
                new() {
                    Id = 2,
                    MeasurementUnitId = 2,
                    PurchaseTaxId = 2,
                    SalesTaxId = 6,
                    PresentationId = 2,
                    EAN = "036000291452",
                    Name = "Aceite Vegetal",
                    Description = "Aceite vegetal en botella",
                    SalesPrice = 12000,
                    NetContent = 2,
                    Manufacturer = "Oliosa",
                    Brand = "Oliosa",
                    Image = "https://picsum.photos/200/300",
                    Quantity = 10
                },
                new() {
                    Id = 3,
                    MeasurementUnitId = 3,
                    PurchaseTaxId = 2,
                    SalesTaxId = 6,
                    PresentationId = 1,
                    EAN = "7702354955793",
                    Name = "Coca cola",
                    Description = "Bebida gaseosa",
                    SalesPrice = 12000,
                    NetContent = 2000,
                    Manufacturer = null,
                    Brand = null,
                    Image = "https://i.etsystatic.com/36867827/r/il/d2ae07/4190387357/il_fullxfull.4190387357_jf6v.jpg",
                    Quantity = 20
                },
                new() {
                    Id = 4,
                    MeasurementUnitId = 3,
                    PurchaseTaxId = 2,
                    SalesTaxId = 6,
                    PresentationId = 2,
                    EAN = "8412345678905",
                    Name = "Aceite premier",
                    Description = "Aceite vegetal en botella",
                    SalesPrice = 28000,
                    NetContent = 1800,
                    Manufacturer = "Premier",
                    Brand = "Premier",
                    Image = "https://picsum.photos/200/300",
                    Quantity = 3
                },
                new() {
                    Id = 5,
                    MeasurementUnitId = 3,
                    PurchaseTaxId = 1,
                    SalesTaxId = 6,
                    PresentationId = 2,
                    EAN = "123456789012",
                    Name = "Pepsi",
                    Description = "Bebida gaseosa de postobon",
                    SalesPrice = 4800,
                    NetContent = 600,
                    Manufacturer = "Postobon",
                    Brand = "Postobon",
                    Image = "https://i.etsystatic.com/36867827/r/il/d2ae07/4190387357/il_fullxfull.4190387357_jf6v.jpg",
                    Quantity = 0
                }
            ];

                // Purchases de ejemplo
                Purchases = new List<Purchase>
            {
                new() {
                    Id = 1,
                    SupplierId = 1,
                    WarehouseId = 1,
                    PaymentMethodId = 2,
                    InvoiceNumber = "FV-001",
                    Date = DateTime.UtcNow.AddDays(-2),
                    Products =
                    [
                        new() { ProductId = 1, Quantity = 12, CostWithoutTax = 4000, CostWithTax = 4200, SalePrice= 4998, PurchaseTax = 0.05m,  TotalPrice = 50400 },
                        new() { ProductId = 2, Quantity = 5, CostWithoutTax = 10000, CostWithTax = 10000, SalePrice= 11900, PurchaseTax = 0m,  TotalPrice = 50000 }
                    ]
                },
                new() {
                    Id = 2,
                    SupplierId = 2,
                    WarehouseId = 2,
                    PaymentMethodId = 2,
                    InvoiceNumber = "FV-002",
                    Date = DateTime.UtcNow.AddDays(-3),
                    Products =
                    [
                        new() { ProductId = 1, Quantity = 12, CostWithoutTax = 4000, CostWithTax = 4200, SalePrice= 4998, PurchaseTax = 0.05m,  TotalPrice = 50400 },
                        new() { ProductId = 2, Quantity = 5, CostWithoutTax = 10000, CostWithTax = 10000, SalePrice= 11900, PurchaseTax = 0m,  TotalPrice = 50000 },
                        new() { ProductId = 4, Quantity = 15, CostWithoutTax = 28000, CostWithTax = 28000, SalePrice= 30859, PurchaseTax = 0m,  TotalPrice = 420000, ProfitPercentage = 0.1m },
                    ]
                }
            };
        }
    }
}
