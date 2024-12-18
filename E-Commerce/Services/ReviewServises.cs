using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using E_Commerce.Repostories;

namespace E_Commerce.Services
{
    public class ReviewServises : IReviewServices
    {
        private readonly IReviewRepo reviewRepo;

        public ReviewServises(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        public string AddReview(ReviewDtoManage review, string userId)
        {
            var item = new Review
            {
                Comment = review.Comment,
                CreatedAt = DateTime.Now,
                ProductId = review.ProductId,
                Rating = review.Rating,
                UserId = userId
            };
            return reviewRepo.AddReview(item);
        }

        public List<ReviewDto> GetUserReviewa(string userId)
        {
            var items = reviewRepo.GetUserReviewa(userId).Select(x => new ReviewDto
            {
                
                Comment=x.Comment,
                ProductId= x.ProductId,
                ProductName=x.Product.Name,
                Rating=x.Rating,
                UserName=x.User.UserName

            }).ToList() ;
            return items;
        }
        public List<ReviewDto> GetProductReview(int id)
        {
            var reviews = reviewRepo.
                GetProductReview(id).Select(x => new ReviewDto
            {
                Comment = x.Comment,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                Rating = x.Rating,
                UserName = x.User.UserName
            }).ToList();

            return reviews;
        }
        public string RemoveReview(int id)
        {
            return reviewRepo.RemoveReview(id);
        }

        public void SaveChanges()
        {
            reviewRepo.SaveChanges();
        }
    }
}
