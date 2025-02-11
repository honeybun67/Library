namespace Library.Data.Models
{
    public class RecommendedBooks
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
