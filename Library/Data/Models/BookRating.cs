﻿namespace Library.Data.Models
{
    public class BookRating
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public double Rating { get; set; }
    }
}
