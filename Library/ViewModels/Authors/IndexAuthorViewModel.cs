using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Authors
{
    public class IndexAuthorViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display (Name = "Picture")]
        public string Image { get; set; }
    }
}
