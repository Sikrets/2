using System;
using Microsoft.EntityFrameworkCore;

namespace ShopApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureCreated();

                Console.WriteLine("=== Workers and their positions/shops ===");
                foreach (var w in db.Workers.Include(w => w.Position).Include(w => w.Shop))
                    Console.WriteLine($"{w.Name} {w.Surname} — {w.Position.Name} at {w.Shop.Name}");

                Console.WriteLine("\n=== Products and their categories ===");
                foreach (var p in db.Products.Include(p => p.Category).Include(p => p.Shop))
                    Console.WriteLine($"{p.Name} ({p.Category?.Name ?? "no category"}) — {p.Price} UAH at {p.Shop.Name}");

                Console.ReadKey();
            }
        }
    }
}