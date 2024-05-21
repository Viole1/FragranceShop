using GS_Parfum.Domain.Entity.Cart;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Interfaces
{
    public interface ICartService
    {
        Task<BaseResponse<Cart>> AddItem(CartItem cartItem);
        Task<BaseResponse<Cart>> DeleteItem(CartItem cartItem);
        Task<BaseResponse<Cart>> GetCartByUserId(int id);
        Task<BaseResponse<bool>> ClearCart(int id);
    }
}
