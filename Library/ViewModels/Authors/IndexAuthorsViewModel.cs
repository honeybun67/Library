namespace Library.ViewModels.Authors
{
    public class IndexAuthorsViewModel : PagingViewModel
    {
        public IndexAuthorsViewModel():base(10)
        {
            
        }
        public IndexAuthorsViewModel(int elementsCount, int itemsPerPage = 10, string action = "Index") : base(elementsCount, itemsPerPage, action)
        {
        }

        public string FilterByName { get; set; }

        public bool IsAsc { get; set; } = true;

        public ICollection<IndexAuthorViewModel> Authors { get; set; } = new List<IndexAuthorViewModel>();
    }
}
