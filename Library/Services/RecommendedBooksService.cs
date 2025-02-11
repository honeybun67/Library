using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Books;
using Library.ViewModels.ReccomendedBooks;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class RecommendedBooksService: IRecommendedBookService
    {
        private readonly ApplicationDbContext context;

        public RecommendedBooksService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<EditRecommendedBookViewModel> GetRecommendedBookToEditAsync(string recBookId)
        {
            RecommendedBooks? recbookModel = await context
                .RecommendedBooks
                .FindAsync(recBookId);

            return new EditRecommendedBookViewModel()
            {
                Id = recBookId,
                Name = recbookModel.Name,
                Rating = recbookModel.Rating
            };
        }
        public async Task<string> UpdateRecBookAsync(EditRecommendedBookViewModel model)
        {
            RecommendedBooks? recBook = await context
                            .RecommendedBooks
                            .FindAsync(model.Id);
            recBook.Name = model.Name;
            recBook.Rating = model.Rating;

            context.RecommendedBooks.Update(recBook);
            await context.SaveChangesAsync();

            return recBook.Id;
        }

        public async Task<string> CreateRecommendedBookAsync(CreateRecommendedBookViewModel model)
        {
            RecommendedBooks recBook = new RecommendedBooks()
            {
                Name= model.Name,
                Author =model.Author,
                Rating = model.Rating,
            };
            await context.RecommendedBooks.AddAsync(recBook);
            await context.SaveChangesAsync();

            return recBook.Id;
        }
        public async Task<IndexRecommendedBooksViewModel> GetRecBooksAsync(IndexRecommendedBooksViewModel model)
        {
            if (model == null)
            {
                model = new IndexRecommendedBooksViewModel(10);
            }
            IQueryable<RecommendedBooks> dataRecBooks = context.RecommendedBooks;

            if (!string.IsNullOrWhiteSpace(model.FilterByName))
            {
                dataRecBooks = dataRecBooks.Where
                    (x => x.Name.Contains(model.FilterByName));
            }

            if (model.IsAsc)
            {
                model.IsAsc = false;
                dataRecBooks.OrderByDescending(x => x.Name);
            }
            else
            {
                model.IsAsc = true;
                dataRecBooks.OrderBy(x => x.Name);
            }
            model.ElementsCount = await dataRecBooks.CountAsync();

            model.RecommendedBooks = await dataRecBooks
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Select(x => new IndexRecommendedBookViewModel()
                {
                    Id = x.Id,
                    Name = x.  Name,
                    Rating = x.Rating
             
                }).ToListAsync();

            return model;
        }
    }
}
