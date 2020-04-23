using Microsoft.EntityFrameworkCore;
using PizzaPortal.Model.Models;

namespace PizzaPortal.Migrations
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PizzaDTO> Pizzas { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ShoppingCartItemDTO> ShoppingCartItems { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
    }
}
