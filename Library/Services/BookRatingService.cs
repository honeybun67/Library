using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Authors;
using Library.ViewModels.BookRatings;
using Library.ViewModels.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookRatingService:IBookRatingsService
    {
        private readonly ApplicationDbContext context;

        public BookRatingService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> CreateBookRatingAsync(CreateBookRatingViewModel model, string userId)
        {
            BookRating bookRating = new BookRating()
            {
                Rating = model.Rating,
                UserId = userId,
                Review = model.Review,
                AuthorId = model.AuthorId
            };

            await context.Ratings.AddAsync(bookRating);
            await context.SaveChangesAsync();

            return bookRating.Id;
        }

        public async Task<IndexBookRatingsUserViewModel> GetUserBookRatingsAsync(IndexBookRatingsUserViewModel model, string userId)
        {
            if (model == null)
            {
                model = new IndexBookRatingsUserViewModel(10);
            }
            IQueryable<BookRating> reviewsData = context.Ratings.Where(x => x.UserId == userId);

            model.ElementsCount = await reviewsData.CountAsync();

            model.UserBookRatings = await reviewsData
              .Skip((model.Page - 1) * model.ItemsPerPage)
              .Take(model.ItemsPerPage)
              .Select(x => new IndexBookRatingViewModel()
              {
                  BookRatingId = x.Id,
                  AuthorName = x.Author.Name,
                  UserId = userId,
                  Rating = x.Rating,
                  Review = x.Review

              }).ToListAsync();

            return model;

        }

    }
}
