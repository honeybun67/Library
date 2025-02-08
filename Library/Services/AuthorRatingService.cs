using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Authors;
using Library.ViewModels.AuthorRatings;
using Library.ViewModels.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorRatingService:IAuthorRatingsService
    {
        private readonly ApplicationDbContext context;

        public AuthorRatingService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> CreateAuthorRatingAsync(CreateAuthorRatingViewModel model, string userId)
        {
            AuthorRating AuthorRating = new AuthorRating()
            {
                Rating = model.Rating,
                UserId = userId,
                Review = model.Review,
                AuthorId = model.AuthorId
            };

            await context.Ratings.AddAsync(AuthorRating);
            await context.SaveChangesAsync();

            return AuthorRating.Id;
        }

        public async Task<IndexAuthorRatingsUserViewModel> GetUserAuthorRatingsAsync(IndexAuthorRatingsUserViewModel model, string userId)
        {
            if (model == null)
            {
                model = new IndexAuthorRatingsUserViewModel(10);
            }
            IQueryable<AuthorRating> reviewsData = context.Ratings.Where(x => x.UserId == userId);

            model.ElementsCount = await reviewsData.CountAsync();

            model.UserAuthorRatings = await reviewsData
              .Skip((model.Page - 1) * model.ItemsPerPage)
              .Take(model.ItemsPerPage)
              .Select(x => new IndexAuthorRatingViewModel()
              {
                  AuthorRatingId = x.Id,
                  AuthorName = x.Author.Name,
                  UserId = userId,
                  Rating = x.Rating,
                  Review = x.Review

              }).ToListAsync();

            return model;

        }

    }
}
