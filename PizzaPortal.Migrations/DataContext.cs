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
    }
}
