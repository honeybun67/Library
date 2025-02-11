using Library.ViewModels.Authors;

namespace Library.Services.Contracts
{
    public interface IAuthorsService
    {
        public Task<string> CreateAuthorAsync(CreateAuthorViewModel model);
        public Task<IndexAuthorsViewModel> GetAuthorsAsync(IndexAuthorsViewModel model);
        public Task<string> UpdateAuthorAsync(EditAuthorViewModel model);
        public Task<EditAuthorViewModel> GetAuthorToEditAsync(string authorId);
      
    }
}
