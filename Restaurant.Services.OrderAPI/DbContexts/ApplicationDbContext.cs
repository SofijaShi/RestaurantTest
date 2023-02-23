using Microsoft.EntityFrameworkCore;
using Restaurant.Services.OrderAPI.Models;

namespace Restaurant.Services.OrderAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
    }
}
