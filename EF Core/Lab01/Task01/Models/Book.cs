namespace Task01.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string? Publisher { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public PriceOffer? PriceOffer { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}