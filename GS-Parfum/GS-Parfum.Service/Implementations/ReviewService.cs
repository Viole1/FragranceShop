using GS_Parfum.DAL.Interfaces;
using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Entity.User;
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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<Review>> AddReview(Review model)
        {
            var baseResponse = new BaseResponse<Review>();
            try
            {
                var user = await _userRepository.Get(model.UserId);
                var review = new Review()
                {
                    ProductId = model.ProductId,
                    UserId = model.UserId,
                    Rate = model.Rate,
                    LongevetyRate = model.LongevetyRate,
                    SillageRate = model.SillageRate,
                    Content = model.Content,
                    Name = user.Name,
                    Date = DateTime.Now,
                };
                await _reviewRepository.Create(review);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Review>()
                {
                    Description = $"[AddReview] : {ex.Message}",
                    ResponseStatus = Domain.Enum.ResponseStatus.UserNotFound,
                };
            }
            baseResponse.ResponseStatus = Domain.Enum.ResponseStatus.OK;
            return baseResponse;
        }

        public async Task<BaseResponse<List<Review>>> GetReviewsByProductId(int productId)
        {
            var baseResponse = new BaseResponse<List<Review>>();
            try
            {
                var reviews = await _reviewRepository.GetReviewsByProductId(productId);
                if (reviews.Count() == 0)
                {
                    baseResponse.Description = "0 elements.";
                    baseResponse.ResponseStatus = ResponseStatus.OK;
                    return baseResponse;
                }
                baseResponse.Data = reviews;
            }

            catch (Exception ex)
            {
                return new BaseResponse<List<Review>>()
                {
                    Description = $"[GetReviewsByProductId] : {ex.Message}",
                    ResponseStatus = ResponseStatus.ProductNotFound,
                };
            }

            baseResponse.ResponseStatus = ResponseStatus.OK;
            return baseResponse;
        }
    }
}
