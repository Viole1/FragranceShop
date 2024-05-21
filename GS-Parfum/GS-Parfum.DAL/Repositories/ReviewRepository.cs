using GS_Parfum.DAL.DbContexts;
using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GS_Parfum.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDbContext _db;
        public ReviewRepository(ReviewDbContext db)
        {
            _db = db;

            _db.Configuration.ProxyCreationEnabled = false;
            _db.Configuration.LazyLoadingEnabled = false;
        }
        public async Task<bool> Create(Review entity)
        {
            _db.Reviews.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Review entity)
        {
            _db.Reviews.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Review> Get(int id)
        {
            return await _db.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Review>> GetReviewsByProductId(int id)
        {
            return await _db.Reviews.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task<List<Review>> Select()
        {
            return await _db.Reviews.ToListAsync();
        }

        public async Task<Review> Update(Review entity)
        {
            _db.Reviews.AddOrUpdate(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
