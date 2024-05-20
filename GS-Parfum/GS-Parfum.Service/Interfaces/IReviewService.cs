using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Parfum.Service.Interfaces
{
    public interface IReviewService
    {
        Task<BaseResponse<Review>> AddReview(Review review);
        Task<BaseResponse<List<Review>>> GetReviewsByProductId(int productId);
    }
}
