using Library.ViewModels.Authors;

namespace Library.ViewModels.Books
{
    public class IndexBooksViewModel : PagingViewModel
    {
        public IndexBooksViewModel():base(10){}
        public IndexBooksViewModel(int elementsCount, int itemsPerPage = 5, string action = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }

        public string FilterByTitle { get; set; }

        public bool IsAsc { get; set; } = true;

        public ICollection<IndexBookViewModel> Books { get; set; } = new List<IndexBookViewModel>();
    }
}
