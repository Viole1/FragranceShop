
using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GS_Parfum.Domain.Entity.Order;
using GS_Parfum.Domain.Entity.Cart;

namespace GS_Parfum.DAL.DbContexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() : base("Host=localhost;Port=5432;Database=GSParfum;Username=postgres;Password=04nykk")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProductDbContext>());
            InitializeDatabase(this);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Chord> Chords { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ProductVolumePrice> ProductVolumePrices { get; set; }
        public DbSet<LongevityRating> LongevityRatings { get; set; }
        public DbSet<SillageRating> SillageRatings { get; set; }
        // public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PRODUCT------------------------------------------------------------
            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // PRODUCT & CHORDS
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Chords)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ProductChords");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("ChordId");
                });
            // PRODUCT & NOTES
            modelBuilder.Entity<Product>()
                .HasMany(p => p.TopNotes)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ProductTopNotes");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("NoteId");
                });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.MiddleNotes)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ProductMiddleNotes");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("NoteId");
                });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.BaseNotes)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ProductBaseNotes");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("NoteId");
                });
            // PRODUCT VOLUMEPRICE
            modelBuilder.Entity<Product>()
                .HasMany(p => p.VolumePrices)
                .WithRequired()
                .HasForeignKey(pvp => pvp.ProductId);
            // LONGEVITY RATING
            modelBuilder.Entity<Product>()
                .HasMany(p => p.LongevityRatings)
                .WithRequired()
                .HasForeignKey(lr => lr.ProductId);
            // SILLAGE RATING
            modelBuilder.Entity<Product>()
                .HasMany(p => p.SillageRatings)
                .WithRequired()
                .HasForeignKey(sr => sr.ProductId);
            // REVIEW-----------------------------------------------------------------------------------------------------------
            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);
        }

        private static void InitializeDatabase(ProductDbContext context)
        {
            if (!context.Chords.Any())
            {
                context.Chords.AddRange(new List<Chord>
                {
                    new Chord { Name = "Древесный" },
                    new Chord { Name = "Амбровый" },
                    new Chord { Name = "Тёплый пряный" }
                });
                context.SaveChanges();
            }
            if (!context.Notes.Any())
            {
                context.Notes.AddRange(new List<Note>
                {
                    new Note { Name = "Уд"},
                    new Note { Name = "Амбра" },
                    new Note { Name = "Ваниль" },
                    new Note { Name = "Сандал" },
                    new Note { Name = "Ветивер" },
                    new Note { Name = "Пачули" },
                    new Note { Name = "Ананас" }
                });
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                var productTomFord = new Product
                {
                    Name = "Tom Ford",
                    Model = "Oud Wood",
                };
                // Загружаем типы продуктов из базы данных
                var woodyChord = context.Chords.FirstOrDefault(t => t.Name == "Древесный");
                var amberChord = context.Chords.FirstOrDefault(t => t.Name == "Амбровый");
                var spicyChord = context.Chords.FirstOrDefault(t => t.Name == "Тёплый пряный");

                var oudNote = context.Notes.FirstOrDefault(t => t.Name == "Уд");
                var amberNote = context.Notes.FirstOrDefault(t => t.Name == "Амбра");
                var santalNote = context.Notes.FirstOrDefault(t => t.Name == "Сандал");

                var volumePrice1 = new ProductVolumePrice { Volume = 50, Price = 100 };
                var volumePrice2 = new ProductVolumePrice { Volume = 75, Price = 150 };



                // Добавляем типы продуктов к продукту Tom Ford
                productTomFord.Chords = new List<Chord> { woodyChord, amberChord };

                productTomFord.TopNotes = new List<Note> { santalNote };
                productTomFord.MiddleNotes = new List<Note> { amberNote };
                productTomFord.BaseNotes = new List<Note> { oudNote };

                productTomFord.VolumePrices = new List<ProductVolumePrice> { volumePrice1, volumePrice2 };


                var longevityRating1 = new LongevityRating { Rating = 1, NumberOfPeople = 4 };
                var longevityRating2 = new LongevityRating { Rating = 5, NumberOfPeople = 400 };

                productTomFord.LongevityRatings = new List<LongevityRating> { longevityRating1, longevityRating2 };

                var sillageRating1 = new SillageRating { Rating = 3, NumberOfPeople = 2 };
                var sillageRating2 = new SillageRating { Rating = 2, NumberOfPeople = 1 };

                productTomFord.SillageRatings = new List<SillageRating> { sillageRating1, sillageRating2 };

                // Добавляем продукт в контекст базы данных и сохраняем изменения
                context.Products.Add(productTomFord);
                context.SaveChanges();

                var productYvesRocher = new Product
                {
                    Name = "Yves Rocher",
                    Model = "Ambre Noir",
                };

                var patchouliNote = context.Notes.FirstOrDefault(t => t.Name == "Пачули");
                var vetiverNote = context.Notes.FirstOrDefault(t => t.Name == "Ветивер");

                var volumePrice3 = new ProductVolumePrice { Volume = 50, Price = 125 };
                var volumePrice4 = new ProductVolumePrice { Volume = 100, Price = 200 };

                productYvesRocher.Chords = new List<Chord> { woodyChord, amberChord };

                productYvesRocher.TopNotes = new List<Note> { vetiverNote };
                productYvesRocher.MiddleNotes = new List<Note> { patchouliNote };
                productYvesRocher.BaseNotes = new List<Note> { amberNote };

                productYvesRocher.VolumePrices = new List<ProductVolumePrice> { volumePrice3, volumePrice4 };


                var longevityRating3 = new LongevityRating { Rating = 4, NumberOfPeople = 111 };
                var longevityRating4 = new LongevityRating { Rating = 5, NumberOfPeople = 4000 };

                productYvesRocher.LongevityRatings = new List<LongevityRating> { longevityRating3, longevityRating4 };


                var sillageRating3 = new SillageRating { Rating = 2, NumberOfPeople = 41 };
                var sillageRating4 = new SillageRating { Rating = 4, NumberOfPeople = 40 };

                productYvesRocher.SillageRatings = new List<SillageRating> { sillageRating3, sillageRating4 };

                context.Products.Add(productYvesRocher);

                context.SaveChanges();
            }
        }
    }
}
