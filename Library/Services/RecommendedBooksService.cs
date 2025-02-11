using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Books;
using Library.ViewModels.ReccomendedBooks;

namespace Library.Services
{
    public class RecommendedBooksService: IRecommendedBookService
    {
        private readonly ApplicationDbContext context;

        public RecommendedBooksService(ApplicationDbContext context)
        {
            this.context = context;
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
       
    }
}
