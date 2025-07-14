using DemoTiendasApi.Models;

namespace DemoTiendasApi.Dtos
{
    public class CreatePurchaseDto
    {
        public int SupplierId { get; set; }
        public int WarehouseId { get; set; }
        public int PaymentMethodId { get; set; }
        public required string InvoiceNumber { get; set; }
        public List<PurchaseProduct> Products { get; set; } = new();
        public decimal? Taxes { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? TransferAmount { get; set; }
        public decimal? CardAmount { get; set; }
    }
}
