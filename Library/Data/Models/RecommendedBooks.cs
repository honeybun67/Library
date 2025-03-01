﻿namespace Library.Data.Models
{
    public class RecommendedBooks
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
    }
}
