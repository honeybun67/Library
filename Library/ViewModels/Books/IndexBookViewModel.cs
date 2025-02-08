using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Books
{
    public class IndexBookViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Picture")]
        public string Image { get; set; }
    }
}
