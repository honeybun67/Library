using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Books
{
    public class CreateBookViewModel
    {
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
