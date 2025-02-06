using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.Authors
{
    public class EditAuthorViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
