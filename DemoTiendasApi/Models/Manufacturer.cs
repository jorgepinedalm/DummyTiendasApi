namespace DemoTiendasApi.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Brand> Brands { get; set; } = [];
    }
}
