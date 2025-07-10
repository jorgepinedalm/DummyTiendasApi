namespace DemoTiendasApi.Models
{
    public class PurchaseProduct
    {
        public int ProductId { get; set; }
        public decimal CostWithoutTax { get; set; }
        public decimal PurchaseTax { get; set; }
        public decimal CostWithTax { get; set; }
        public int Quantity { get; set; }
        public decimal ProfitPercentage { get; set; } //porcentaje de ganancia
        public decimal SalePrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}