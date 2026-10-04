using Microsoft.EntityFrameworkCore;

namespace ShopApp
{
    public class ShopContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopFluentDB_v2;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(e =>
            {
                e.ToTable("Countries");
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<City>(e =>
            {
                e.ToTable("Cities");
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);

                e.HasOne(c => c.Country)
                 .WithMany(c => c.Cities)
                 .HasForeignKey(c => c.CountryId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Shop>(e =>
            {
                e.ToTable("Shops");
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired().HasMaxLength(100);
                e.Property(s => s.Address).IsRequired().HasMaxLength(200);
                e.Property(s => s.ParkingArea).IsRequired(false);

                e.HasOne(s => s.City)
                 .WithMany(c => c.Shops)
                 .HasForeignKey(s => s.CityId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Position>(e =>
            {
                e.ToTable("Positions");
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Worker>(e =>
            {
                e.ToTable("Workers");
                e.HasKey(w => w.Id);
                e.Property(w => w.Name).IsRequired().HasMaxLength(50);
                e.Property(w => w.Surname).IsRequired().HasMaxLength(50);
                e.Property(w => w.Salary).HasColumnType("decimal(18,2)");
                e.Property(w => w.Email).HasMaxLength(100);
                e.Property(w => w.PhoneNumber).HasMaxLength(20);

                e.HasOne(w => w.Position)
                 .WithMany(p => p.Workers)
                 .HasForeignKey(w => w.PositionId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(w => w.Shop)
                 .WithMany(s => s.Workers)
                 .HasForeignKey(w => w.ShopId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired().HasMaxLength(100);
                e.Property(p => p.Price).HasColumnType("decimal(18,2)");
                e.Property(p => p.Discount).HasColumnType("real");
                e.Property(p => p.CategoryId).IsRequired(false);

                e.HasOne(p => p.Shop)
                 .WithMany(s => s.Products)
                 .HasForeignKey(p => p.ShopId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Ukraine" },
                new Country { Id = 2, Name = "Poland" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Kyiv", CountryId = 1 },
                new City { Id = 2, Name = "Lviv", CountryId = 1 },
                new City { Id = 3, Name = "Warsaw", CountryId = 2 }
            );

            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Manager" },
                new Position { Id = 2, Name = "Salesperson" },
                new Position { Id = 3, Name = "Security Guard" }
            );

            modelBuilder.Entity<Shop>().HasData(
                new Shop { Id = 1, Name = "Silpo", Address = "1 Khreshchatyk St.", CityId = 1, ParkingArea = 50 },
                new Shop { Id = 2, Name = "ATB", Address = "10 Soborna St.", CityId = 2, ParkingArea = null }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Groceries" },
                new Category { Id = 2, Name = "Home Appliances" }
            );

            modelBuilder.Entity<Worker>().HasData(
                new Worker { Id = 1, Name = "Ivan", Surname = "Petrenko", Salary = 15000, Email = "ivan@mail.com", PhoneNumber = "+380501112233", PositionId = 1, ShopId = 1 },
                new Worker { Id = 2, Name = "Maria", Surname = "Ivanenko", Salary = 12000, Email = "maria@mail.com", PhoneNumber = "+380502223344", PositionId = 2, ShopId = 1 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Milk", Price = 35.50m, Discount = 5, Quantity = 100, IsInStock = true, ShopId = 1, CategoryId = 1 },
                new Product { Id = 2, Name = "TV", Price = 15000m, Discount = 10, Quantity = 5, IsInStock = true, ShopId = 2, CategoryId = 2 },
                new Product { Id = 3, Name = "Bread", Price = 20m, Discount = 0, Quantity = 50, IsInStock = true, ShopId = 1, CategoryId = 1 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}