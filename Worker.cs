namespace ShopApp
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = null!;
    }
}