using DemoTiendasApi.Models;

namespace DemoTiendasApi.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Supplier? Supplier { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public required string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public List<PurchaseProductDto> Products { get; set; } = new();
        public decimal? CashAmount { get; set; }
        public decimal? TransferAmount { get; set; }
        public decimal? CardAmount { get; set; }
    }
    public class PurchaseProductDto
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
