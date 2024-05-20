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
		public UserDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=13579")
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
					Name = "AdminName",
					Surname ="AdminSurname",
					Username = "Admin",
					Password = "12345", //HashPasswordHelper.HashPassword("12345"),
					Role = Role.ROLE_ADMIN,
					Email = "admin@gmail.com",
					PhoneNumber = "12345",
					DeliveryName = "AdminDeliveryName",
					DeliveryCity = "AdminDeliveryCity",
					DeliveryStreet = "AdminDeliveryStreet",
					DeliveryHomeNumber = "12",
				});
                context.Users.Add(new User
                {
                    Name = "UserName",
                    Surname = "UserSurname",
                    Username = "User",
                    Password = "12345", //HashPasswordHelper.HashPassword("12345"),
                    Role = Role.ROLE_USER,
                    Email = "user@gmail.com",
                    PhoneNumber = "12345",
                    DeliveryName = "UserDeliveryName",
                    DeliveryCity = "UserDeliveryCity",
                    DeliveryStreet = "UserDeliveryStreet",
                    DeliveryHomeNumber = "34",
                });

                context.SaveChanges();
			}
		}
	}
}
