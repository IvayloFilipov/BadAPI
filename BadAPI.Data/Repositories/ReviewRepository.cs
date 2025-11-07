using BadApi.Data;
using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BadAPI.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BadDbContext context;

        public ReviewRepository(BadDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId)
        {
            var reviews = await context.Reviews.Where(r => r.ProductId == productId).ToListAsync();

            return reviews;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                throw new InvalidOperationException($"Review with id {id} not found.");
            }  

            return review;
        }

        public async Task AddReviewAsync(Review review)
        {
            await context.Reviews.AddAsync(review);
        } 

        public async Task UpdateReviewAsync(Review review)
        {
            context.Reviews.Update(review);

            await Task.CompletedTask;
        }

        public async Task DeleteReviewAsync(Review review)
        {
            context.Reviews.Remove(review);

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> ProductHasReviewsAsync(int productId)
        {
            var booleanResult = await context.Reviews.AnyAsync(r => r.ProductId == productId);

            return booleanResult;
        }
    }
}
