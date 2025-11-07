using BadAPI.Data.Entities;

namespace BadAPI.Data.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId);
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
        Task SaveChangesAsync();

        // Added this after refactoring to check if a product has any reviews
        Task<bool> ProductHasReviewsAsync(int productId);
    }
}
