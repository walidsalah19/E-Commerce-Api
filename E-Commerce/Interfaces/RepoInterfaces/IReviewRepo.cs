using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IReviewRepo
    {
        public string AddReview(Review review);
        public List<Review> GetUserReviewa(string userId);
        public string RemoveReview(int id);
        public Review GetById(int id);
        public List<Review> GetProductReview(int id);

        public void SaveChanges();
    }
}
