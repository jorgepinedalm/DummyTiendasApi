using DemoTiendasApi.Models;

namespace DemoTiendasApi.Services
{
    public class InMemoryDataService
    {
        public List<Product> Products { get; set; } = new();
        public List<Purchase> Purchases { get; set; } = new();
    }
}
