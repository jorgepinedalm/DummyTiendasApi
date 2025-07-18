﻿namespace DemoTiendasApi.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int WarehouseId { get; set; }
        public int PaymentMethodId { get; set; }
        public required string InvoiceNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public List<PurchaseProduct> Products { get; set; } = new();
        public decimal? Taxes { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? TransferAmount { get; set; }
        public decimal? CardAmount { get; set; }
    }
}
