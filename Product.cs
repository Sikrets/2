namespace ShopApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int Quantity { get; set; }
        public bool IsInStock { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = null!;
    }
}