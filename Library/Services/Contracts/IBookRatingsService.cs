using Library.ViewModels.BookRatings;
using Library.ViewModels.Ratings;

namespace Library.Services.Contracts
{
    public interface IBookRatingsService
    {
        public Task<string> CreateBookRatingAsync(CreateBookRatingViewModel model, string userId);

        public Task<IndexBookRatingsUserViewModel> GetUserBookRatingsAsync(IndexBookRatingsUserViewModel model, string userId);
    }
}
