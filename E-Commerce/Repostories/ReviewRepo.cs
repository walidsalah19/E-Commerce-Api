using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDbContext context;

        public ReviewRepo(AppDbContext context)
        {
            this.context = context;
        }

        public string AddReview(Review review)
        {
            try
            {
                context.Reviews.Add(review);
                return "Success";
            }catch(Exception e)
            {
                return e.Message;
            }
        }

        public Review GetById(int id)
        {
            return context.Reviews.FirstOrDefault(x => x.ReviewId == id);
        }

        public List<Review> GetUserReviewa(string userId)
        {
            var items = context.Reviews.Include(x => x.Product).Include(x => x.User).Where(x => x.UserId.Equals(userId)).ToList();
            return items;
        }
        public List<Review> GetProductReview(int id)
        {
            return context.Reviews.Include(x => x.Product).Include(x => x.User).Where(x=>x.ProductId==id).ToList();
        }
        public string RemoveReview(int id)
        {
            try
            {
                var review =GetById(id);
                if (review is not null)
                {
                    context.Reviews.Remove(review);
                    return "Success";
                }
                return "Not Found";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
