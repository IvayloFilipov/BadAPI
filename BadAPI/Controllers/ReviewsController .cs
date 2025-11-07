using BadAPI.Data.Entities;
using BadAPI.Services.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BadAPI.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/reviews")]
    public class ReviewsController : ControllerBase 
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet("getReview")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await reviewService.GetProductReviewsAsync(productId);

            return Ok(reviews);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview(int productId, ReviewCreateDto dto)
        {
            var review = new Review
            {
                ProductId = productId,
                Rating = (Data.Enums.Rating)dto.Rating,
                Comment = dto.Comment,
                ReviewerName = dto.ReviewerName
            };

            var created = await reviewService.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReviews), new { productId }, created);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int id, ReviewUpdateDto dto)
        {
            var updated = await reviewService.UpdateReviewAsync(id, new Review
            {
                Rating = (Data.Enums.Rating)dto.Rating,
                Comment = dto.Comment,
                ReviewerName = dto.ReviewerName
            });

            if (updated == null) 
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await reviewService.DeleteReviewAsync(id);

            if (!success) 
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
