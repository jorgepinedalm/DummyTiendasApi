﻿namespace DemoTiendasApi.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DocTypeId { get; set; }
        public required string Identification { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
