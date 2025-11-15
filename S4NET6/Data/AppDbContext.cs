using Microsoft.EntityFrameworkCore;
using S4NET6.Models;

namespace S4NET6.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
