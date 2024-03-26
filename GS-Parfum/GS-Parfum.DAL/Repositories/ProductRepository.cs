using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> Select()
        {
            // Include нужен для загрузки связанных полей
            return await _db.Products
                .Include(p => p.Types)
                .Include(p => p.Chords)
                .Include(p => p.TopNotes)
                .Include(p => p.MiddleNotes)
                .Include(p => p.BaseNotes)
                .Include(p => p.VolumePrices)
                .Include(p => p.LongevityRatings)
                .Include(p => p.SillageRatings)
                .Include(p => p.Reviews)
                .ToListAsync();
        }

        public async Task<bool> Create(Product entity)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Product> Update(Product entity)
        {
            _db.Products.AddOrUpdate(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
