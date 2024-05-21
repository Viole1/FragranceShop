using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Order;
using GS_Parfum.Domain.Enum;
using GS_Parfum.Domain.Response;
using GS_Parfum.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartReporistory;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartReporistory, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _cartReporistory = cartReporistory;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<Order>> CreateOrder(Order model)
        {
            var baseResponse = new BaseResponse<Order>();
            try
            {
                var cart = await _cartReporistory.Get(model.CartId);
                var user = await _userRepository.Get(model.UserId);
                var order = new Order()
                {
                    CartItems = cart.Items,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.IN_PROCESS,
                    UserId = model.UserId,
                    CartId = cart.Id,
                    TotalPrice = cart.TotalPrice,
                    DeliveryName = user.DeliveryName,
                    DeliveryCity = user.DeliveryCity,
                    DeliveryStreet = user.DeliveryStreet,
                    DeliveryHomeNumber = user.DeliveryHomeNumber,
                };

                await _orderRepository.Create(order);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = $"[CreateOrder] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                };
            }
            baseResponse.ResponseStatus = ResponseStatus.OK;
            return baseResponse;
        }

        public async Task<BaseResponse<List<Order>>> GetOrdersByUserId(int id)
        {
            var baseResponse = new BaseResponse<List<Order>>();
            try
            {
                var order = await _orderRepository.GetOrdersByUserId(id);
                if (order == null)
                {
                    baseResponse.Description = "Orders not found";
                    baseResponse.ResponseStatus = ResponseStatus.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = order.ToList();
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Order>>()
                {
                    Description = $"[GetOrders] : {ex.Message}",
                };
            }
        }
    }
}
