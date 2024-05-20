using GS_Parfum.Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.DbContexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=13579")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OrderDbContext>());
        }

        public DbSet<Order> Orders { get; set; }
    }
}
