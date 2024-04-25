using GS_Parfum.DAL.DbContexts;
using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _db;

        public OrderRepository(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Order entity)
        {
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Order> Get(int id)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int id)
        {
            return await _db.Orders.Include(i => i.CartItems).Where(o => o.UserId == id).ToListAsync();
        }

        public async Task<List<Order>> Select()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order> Update(Order entity)
        {
            _db.Orders.AddOrUpdate(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
