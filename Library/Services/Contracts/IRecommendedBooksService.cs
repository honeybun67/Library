using Library.ViewModels.ReccomendedBooks;

namespace Library.Services.Contracts
{
    public interface IRecommendedBookService
    {
        public Task<string> CreateRecommendedBookAsync(CreateRecommendedBookViewModel model);
        public Task<IndexRecommendedBooksViewModel> GetBooksAsync(IndexRecommendedBooksViewModel model);
    }
}
