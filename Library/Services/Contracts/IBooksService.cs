using Library.ViewModels.Books;

namespace Library.Services.Contracts
{
    public interface IBooksService
    {
        public Task<string> CreateBookAsync(CreateBookViewModel model);
        public Task<IndexBooksViewModel> GetBooksAsync(IndexBooksViewModel model);

        public Task<string> UpdateBookAsync(EditBookViewModel model);

        public Task<EditBookViewModel> GetBookToEditAsync(string bookId);

    }
}
