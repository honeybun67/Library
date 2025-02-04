using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.ViewModels.Authors;

namespace Library.Services
{
    public class AuthorService:IAuthorsService
    {
        private readonly ApplicationDbContext context;

        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
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
