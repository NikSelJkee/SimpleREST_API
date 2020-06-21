using Microsoft.EntityFrameworkCore;
using SimpleREST_API.Entities;

namespace SimpleREST_API.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        { 
        }
    }
}
