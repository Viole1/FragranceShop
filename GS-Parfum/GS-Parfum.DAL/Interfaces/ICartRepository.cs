using GS_Parfum.Domain.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.DAL.Interfaces
{
    public interface ICartRepository :IBaseRepository<Cart>
    {
        Task<Cart> GetCartByUserId(int id);
    }
}
