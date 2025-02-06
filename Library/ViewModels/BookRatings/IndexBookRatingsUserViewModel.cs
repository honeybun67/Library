namespace Library.ViewModels.BookRatings
{
    public class IndexBookRatingsUserViewModel:PagingViewModel
    {
        public IndexBookRatingsUserViewModel():base(10)
        {
            
        }
        public IndexBookRatingsUserViewModel(int elementsCount, int itemsPerPage = 5, string action
            = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }

        public ICollection<IndexBookRatingViewModel> UserBookRatings { get; set; } =
            new HashSet<IndexBookRatingViewModel>();
    }
}
