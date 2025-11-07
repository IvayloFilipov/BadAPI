using BadAPI.Data.Entities;

namespace BadAPI.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetProductReviewsAsync(int productId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(int id, Review updatedReview);
        Task<bool> DeleteReviewAsync(int id);
    }
}
