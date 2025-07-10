namespace DemoTiendasApi.Dtos
{
    public class PurchaseSummaryDto
    {
        public int Id { get; set; }
        public required string WarehouseName { get; set; }
        public required string InvoiceNumber { get; set; }
        public required string SupplierName { get; set; }
        public required string Date { get; set; } // Formato yyyy-MM-dd HH:mm:ss
        public required string Charges { get; set; } // "19%"
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
