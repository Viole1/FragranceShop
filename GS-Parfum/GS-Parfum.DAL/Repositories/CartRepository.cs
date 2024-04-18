using GS_Parfum.DAL.DbContexts;
using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _db;
        public CartRepository(CartDbContext db)
        {
            _db = db;
        }

        public async Task<Cart> GetCartByUserId(int id)
        {
            try
            {
                return await _db.Carts.Include(i => i.Items).FirstOrDefaultAsync(x => x.UserId == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Create(Cart entity)
        {
            _db.Carts.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> Get(int id)
        {
            return await _db.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Cart>> Select()
        {
            return await _db.Carts.ToListAsync();
        }

        public async Task<bool> Delete(Cart entity)
        {
            _db.Carts.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> Update(Cart entity)
        {
            _db.Carts.AddOrUpdate(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
