namespace Task01
{
    internal class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public Book(string _ISBN, string _Title, string[] _Authors, DateTime _PublicationDate, decimal _Price)
        {
            this.ISBN = _ISBN;
            this.Title = _Title;
            this.Authors = _Authors;
            this.PublicationDate = _PublicationDate;
            this.Price = _Price;
        }
        public override string ToString() => $"{Title} | {ISBN} | {string.Join(", ", Authors)} | {PublicationDate.ToShortDateString()} | {Price:C}";
    }
}
