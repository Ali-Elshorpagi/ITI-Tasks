namespace Task01
{
    internal interface IBookService
    {
        public string GetTitle(Book B);
        public string GetAuthors(Book B);
        public string GetPrice(Book B);
    }
}
