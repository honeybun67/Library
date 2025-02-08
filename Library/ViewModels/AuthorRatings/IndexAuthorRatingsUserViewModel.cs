namespace Library.ViewModels.AuthorRatings
{
    public class IndexAuthorRatingsUserViewModel:PagingViewModel
    {
        public IndexAuthorRatingsUserViewModel():base(10)
        {
            
        }
        public IndexAuthorRatingsUserViewModel(int elementsCount, int itemsPerPage = 5, string action
            = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }

        public ICollection<IndexAuthorRatingViewModel> UserAuthorRatings { get; set; } =
            new HashSet<IndexAuthorRatingViewModel>();
    }
}
