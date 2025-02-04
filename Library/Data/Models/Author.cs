using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Author
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength (64)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Image { get; set; }

    }
}
