using GS_Parfum.Domain.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.DbContexts
{
    public class CartDbContext : DbContext
    {
        public CartDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=04nykk")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CartDbContext>());
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
