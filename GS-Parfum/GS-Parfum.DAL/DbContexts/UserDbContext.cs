using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GS_Parfum.Domain.Entity.User;
using GS_Parfum.Domain.Enum;

namespace GS_Parfum.DAL.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=04nykk")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<UserDbContext>());
            InitializeDatabase(this);
        }
        public DbSet<User> Users { get; set; }
        private static void InitializeDatabase(UserDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "Admin",
                    Password = "12345", //HashPasswordHelper.HashPassword("12345"),
                    Role = Role.ROLE_ADMIN
                });

                context.SaveChanges();
            }
        }
    }
}
