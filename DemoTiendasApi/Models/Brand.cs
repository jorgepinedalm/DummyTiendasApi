namespace DemoTiendasApi.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public required string Name { get; set; }
    }
}
