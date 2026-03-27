using System;
using System.Collections.Generic;
using System.Text;

namespace Task01
{
    internal class BookService : IBookService
    {
        public string GetAuthors(Book B) => string.Join(",", B.Authors);
        public string GetPrice(Book B) => B.Price.ToString("C");
        public string GetTitle(Book B) => B.Title;
    }
}
