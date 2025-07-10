namespace DemoTiendasApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int MeasurementUnitId { get; set; }
        public int PurchaseTaxId { get; set; }
        public int SalesTaxId { get; set; }
        public int PresentationId { get; set; }
        public required string EAN { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal NetContent { get; set; }
        public string? Manufacturer { get; set; }
        public string? Brand { get; set; }
        public string? Image { get; set; }
    }
}
