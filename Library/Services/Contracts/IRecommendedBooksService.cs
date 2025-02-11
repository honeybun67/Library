using Library.ViewModels.ReccomendedBooks;

namespace Library.Services.Contracts
{
    public interface IRecommendedBookService
    {
        public Task<string> CreateRecommendedBookAsync(CreateRecommendedBookViewModel model);
        public Task<IndexRecommendedBooksViewModel> GetRecBooksAsync(IndexRecommendedBooksViewModel model);
        public Task<EditRecommendedBookViewModel> GetRecommendedBookToEditAsync(string recBookId);
        public Task<string> UpdateRecBookAsync(EditRecommendedBookViewModel model);
    }
}
