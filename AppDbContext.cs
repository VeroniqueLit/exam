using Microsoft.EntityFrameworkCore;
using exam.Models;

namespace exam
{
    public class AppDbContext : DbContext
    {
        public DbSet<RectangleItem> Rectangles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=examDB;Username=postgres;Password=qwerty1234");
        }
    }
}