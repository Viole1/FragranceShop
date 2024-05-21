using GS_Parfum.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.DbContexts
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=13579")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReviewDbContext>());
        }

        public DbSet<Review> Reviews { get; set; }
    }
}
