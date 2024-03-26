using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponse<Product>> GetProduct(int id);
        Task<BaseResponse<Product>> GetProductByName(string name);
        Task<BaseResponse<Product>> CreateProduct(Product model);
        Task<BaseResponse<Product>> Edit(int id, Product model);
        Task<BaseResponse<bool>> DeleteProduct(int id);
        Task<BaseResponse<IEnumerable<Product>>> Select();

    }
}
