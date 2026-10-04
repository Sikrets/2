using System.Collections.Generic;

namespace ShopApp
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
        public City City { get; set; } = null!;
        public int? ParkingArea { get; set; }
        public List<Worker> Workers { get; set; } = new List<Worker>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}