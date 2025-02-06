using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Authors;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class AuthorService:IAuthorsService
    {
        private readonly ApplicationDbContext context;

        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> UpdateAuthorAsync(EditAuthorViewModel model)
        {
            Author? author = await context
                            .Authors
                            .FindAsync(model.Id);
            author.Name = model.Name;
            author.Description = model.Description;
            context.Authors.Update(author);
           await context.SaveChangesAsync();
            return author.Id;
        }
        public async Task<EditAuthorViewModel> GetAuthorToEditAsync(string authorId) 
        {
            Author? author =await context
                .Authors
                .FindAsync(authorId);

         return new EditAuthorViewModel()
            {
                Id = authorId,
                Name= author.Name,
                Description = author.Description
            };             
        }
        public async Task<string> CreateAuthorAsync(CreateAuthorViewModel model) 
        {
            Author author = new Author()
            { 
                Name = model.Name,
                Description = model.Description,
                Image = await ImageToStringAsync(model.ImageFile)
            
            };
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();

            return author.Id;
        }

        public async Task<IndexAuthorsViewModel> GetAuthorsAsync(IndexAuthorsViewModel model)
        {
            if (model == null)
            {
                model = new IndexAuthorsViewModel(10);
            }
            IQueryable<Author> dataAuthors = context.Authors;

            if (!string.IsNullOrWhiteSpace(model.FilterByName))
            {
                dataAuthors = dataAuthors.Where
                    (x => x.Name.Contains(model.FilterByName));
            }

            if (model.IsAsc)
            {
                model.IsAsc = false;
                dataAuthors.OrderByDescending(x => x.Name);
            }
            else
            {
                model.IsAsc = true;
                dataAuthors.OrderBy(x => x.Name);
            }
             model.ElementsCount = await dataAuthors.CountAsync();

            model.Authors = await dataAuthors
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .Select(x => new IndexAuthorViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
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
