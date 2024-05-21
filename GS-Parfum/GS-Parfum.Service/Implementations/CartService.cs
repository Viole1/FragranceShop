using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Cart;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<Cart>> AddItem(CartItem cartItem)
        {
            var baseResponse = new BaseResponse<Cart>();
            try
            {
                var cart = await _cartRepository.GetCartByUserId(cartItem.UserId);
                var volumePrice = await _productRepository.GetVolumePriceById(cartItem.VolumePriceId);
                if (cart != null)
                {
                    cart.Items.Add(cartItem);
                    cart.TotalPrice += volumePrice.Price;
                    await _cartRepository.Update(cart);
                }
                else
                {
                    cart = new Cart();
                    cart.Items = new List<CartItem>() { cartItem };
                    cart.TotalPrice += volumePrice.Price;
                    cart.UserId = cartItem.UserId;
                    await _cartRepository.Create(cart);
                }
                baseResponse.Data = cart;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Cart>()
                {
                    Description = $"[AddItem] : {ex.Message}",
                    ResponseStatus = Domain.Enum.ResponseStatus.InternalServerError,
                };
            }
            baseResponse.ResponseStatus = Domain.Enum.ResponseStatus.OK;
            return baseResponse;
        }

        public Task<BaseResponse<Cart>> DeleteItem(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Cart>> GetCartByUserId(int id)
        {
            var baseResponse = new BaseResponse<Cart>();
            try
            {
                var cart = await _cartRepository.GetCartByUserId(id);
                if (cart == null)
                {
                    baseResponse.Description = "Cart not found";
                    baseResponse.ResponseStatus = ResponseStatus.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = cart;
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Cart>()
                {
                    Description = $"[GetCart] : {ex.Message}",
                };
            }
        }
        public async Task<BaseResponse<bool>> ClearCart(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                baseResponse.Data = await _cartRepository.ClearCart(id);
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ClearCart] : {ex.Message}",
                };
            }
        }
    }
}
