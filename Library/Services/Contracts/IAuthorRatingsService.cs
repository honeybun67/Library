using Library.ViewModels.AuthorRatings;
using Library.ViewModels.Ratings;

namespace Library.Services.Contracts
{
    public interface IAuthorRatingsService
    {
        public Task<string> CreateAuthorRatingAsync(CreateAuthorRatingViewModel model, string userId);

        public Task<IndexAuthorRatingsUserViewModel> GetUserAuthorRatingsAsync(IndexAuthorRatingsUserViewModel model, string userId);
        public Task SeedReviewsAsync();
    }
}
