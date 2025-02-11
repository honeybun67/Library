using Library.ViewModels.Books;

namespace Library.ViewModels.ReccomendedBooks
{
    public class IndexRecommendedBooksViewModel : PagingViewModel
    {
        public IndexRecommendedBooksViewModel() : base(10) { }
        public IndexRecommendedBooksViewModel(int elementsCount, int itemsPerPage = 5, string action = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }
        public string FilterByName { get; set; }

        public bool IsAsc { get; set; } = true;

        public ICollection<IndexRecommendedBookViewModel> RecommendedBooks { get; set; } = new List<IndexRecommendedBookViewModel>();
    }
}
