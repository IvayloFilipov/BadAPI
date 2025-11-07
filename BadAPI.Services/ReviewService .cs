using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;

using static Common.GlobalConstants;

namespace BadAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepo;

        public ReviewService(IReviewRepository reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        public async Task<IEnumerable<Review>> GetProductReviewsAsync(int productId)
        {
            var reviews = await reviewRepo.GetReviewsForProductAsync(productId);

            return reviews;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            if ((int)review.Rating < 1 || (int)review.Rating > 5)
                throw new ArgumentException(Rating_Must_Be_Between_1_And_5);

            if (string.IsNullOrWhiteSpace(review.Comment))
                throw new ArgumentException(Comment_Cannot_Be_Empty);

            if (string.IsNullOrWhiteSpace(review.ReviewerName))
                throw new ArgumentException(Reviewer_Name_Is_Required);

            await reviewRepo.AddReviewAsync(review);
            await reviewRepo.SaveChangesAsync();

            return review;
        }

        public async Task<Review> UpdateReviewAsync(int id, Review updatedReview)
        {
            var existingReview = await reviewRepo.GetReviewByIdAsync(id);

            if (existingReview == null) return null;

            existingReview.Rating = updatedReview.Rating; 
            existingReview.Comment = updatedReview.Comment;
            existingReview.ReviewerName = updatedReview.ReviewerName;

            await reviewRepo.UpdateReviewAsync(existingReview);
            await reviewRepo.SaveChangesAsync();

            return existingReview;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var existingReview = await reviewRepo.GetReviewByIdAsync(id);

            if (existingReview == null) return false;

            await reviewRepo.DeleteReviewAsync(existingReview);
            await reviewRepo.SaveChangesAsync();

            return true;
        }
    }
}
