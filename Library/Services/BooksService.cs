using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Authors;
using Library.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BooksService:IBooksService
    {
        private readonly ApplicationDbContext context;

        public BooksService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> UpdateBookAsync(EditBookViewModel model)
        {
            Book? book = await context
                            .Books
                            .FindAsync(model.Id);
            book.Title = model.Title;
            book.Description = model.Description;

            context.Books.Update(book);
            await context.SaveChangesAsync();

            return book.Id;
        }

        public async Task<EditBookViewModel> GetBookToEditAsync(string bookId)
        {
            Book? book = await context
                .Books
                .FindAsync(bookId);

            return new EditBookViewModel()
            {
                Id = bookId,
                Title = book.Title,
                Description = book.Description
            };
        }


        public async Task<string> CreateBookAsync(CreateBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Image = await ImageToStringAsync(model.ImageFile)

            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return book.Id;

        }
        public async Task<IndexBooksViewModel> GetBooksAsync(IndexBooksViewModel model)
        {
            if (model == null)
            {
                model = new IndexBooksViewModel(10);
            }
            IQueryable<Book> dataBooks = context.Books;

            if (!string.IsNullOrWhiteSpace(model.FilterByTitle))
            {
                dataBooks = dataBooks.Where
                    (x => x.Title.Contains(model.FilterByTitle));
            }

            if (model.IsAsc)
            {
                model.IsAsc = false;
                dataBooks.OrderByDescending(x => x.Title);
            }
            else
            {
                model.IsAsc = true;
                dataBooks.OrderBy(x => x.Title);
            }
            model.ElementsCount = await dataBooks.CountAsync();

            model.Books = await dataBooks
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Select(x => new IndexBookViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                }).ToListAsync();

            return model;
        }

        private async Task<string> ImageToStringAsync(IFormFile file)
        {
            List<string> imageExtensions = new List<string>() { ".JPG", ".BMP", ".PNG" };


            if (file != null) // check if the user uploded something
            {
                var extension = Path.GetExtension(file.FileName); //get file extension
                if (imageExtensions.Contains(extension.ToUpperInvariant()))
                {
                    using var dataStream = new MemoryStream();
                    await file.CopyToAsync(dataStream);
                    byte[] imageBytes = dataStream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            return null;
        }


    }
}
