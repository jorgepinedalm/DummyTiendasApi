namespace DemoTiendasApi.Dtos
{
    public class ProductUpdateDto
    {
        public int MeasurementUnitId { get; set; }
        public int PurchaseTaxId { get; set; }
        public int SalesTaxId { get; set; }
        public int PresentationId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal NetContent { get; set; }
        public string? Manufacturer { get; set; }
        public string? Brand { get; set; }
        public IFormFile? Image { get; set; }
    }
}
