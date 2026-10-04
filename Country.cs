using System.Collections.Generic;

namespace ShopApp
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<City> Cities { get; set; } = new List<City>();
    }
}