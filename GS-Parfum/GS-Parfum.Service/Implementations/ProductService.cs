using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Product;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<Product>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.ResponseStatus = ResponseStatus.ProductNotFound;
                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;            
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                };
            }
        }

        public async Task<BaseResponse<Product>> GetProductByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.GetByName(name);
                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.ResponseStatus = ResponseStatus.ProductNotFound;
                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Product>>> Select()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var products = await _productRepository.Select();
                if (products.Count() == 0)
                {
                    baseResponse.Description = "0 elements.";
                    baseResponse.ResponseStatus = ResponseStatus.SelectFailed;
                    return baseResponse;
                }

                baseResponse.Data = products;
                baseResponse.ResponseStatus = ResponseStatus.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                };
            }
        }

        public async Task<BaseResponse<Product>> CreateProduct(Product model)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = new Product()
                {
                    Photo = model.Photo,
                    Name = model.Name,
                    Model = model.Model,
                    Sex = (SexType)Convert.ToInt32(model.Sex),
                    Rating = model.Rating,
                    NumberOfRatings = model.NumberOfRatings,
                    Country = model.Country,
                    Description = model.Description,
                };

                await _productRepository.Create(product);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[ProductViewModel] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                };
            }
            baseResponse.ResponseStatus = ResponseStatus.OK;
            return baseResponse;
        }

        public async Task<BaseResponse<Product>> Edit(int id, Product model)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "Product not found.";
                    baseResponse.ResponseStatus = ResponseStatus.ProductNotFound;
                    return baseResponse;
                }

                product.Photo = model.Photo;
                product.Name = model.Name;
                product.Model = model.Model;
                product.Sex = (SexType)Convert.ToInt32(model.Sex);
                product.Rating = model.Rating;
                product.NumberOfRatings = model.NumberOfRatings;
                product.Country = model.Country;
                product.Description = model.Description;

                await _productRepository.Update(product);
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.ResponseStatus = ResponseStatus.UserNotFound;
                    return baseResponse;
                }

                await _productRepository.Delete(product);
                baseResponse.Data = true;
                baseResponse.ResponseStatus = ResponseStatus.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    ResponseStatus = ResponseStatus.InternalServerError,
                };
            }
        }
    }
}
