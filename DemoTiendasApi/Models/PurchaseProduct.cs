namespace DemoTiendasApi.Models
{
    public class PurchaseProduct
    {
        public int ProductId { get; set; }
        public float CostWithoutTax { get; set; }
        public float PurchaseTax { get; set; }
        public float CostWithTax { get; set; }
        public int Quantity { get; set; }
        public float ProfitPercentage { get; set; } //porcentaje de ganancia
        public float SalePrice { get; set; }
    }
}