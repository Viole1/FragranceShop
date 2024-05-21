using GS_Parfum.Domain.Entity.Order;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> CreateOrder(Order order);
        Task<BaseResponse<List<Order>>> GetOrdersByUserId(int id);
    }
}
