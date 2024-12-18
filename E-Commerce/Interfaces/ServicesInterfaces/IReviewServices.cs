using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface IReviewServices
    {
        public string AddReview(ReviewDtoManage review, string userId);
        public List<ReviewDto> GetUserReviewa(string userId);
        public string RemoveReview(int id);
        public List<ReviewDto> GetProductReview(int id);

        public void SaveChanges();
    }
}
