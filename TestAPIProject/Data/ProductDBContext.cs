using Microsoft.EntityFrameworkCore;
using TestAPIProject.Models;

namespace TestAPIProject.Data
{
    public class ProductDBContext:DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> context) : base(context)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserLogin> Login { get; set; }
    }
}
