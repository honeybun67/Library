namespace Library.Data.Models
{
    public class BookReating
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public double Rating { get; set; }

        public string Review { get; set; }
    }
}
