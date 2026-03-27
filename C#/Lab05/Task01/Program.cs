using static Task01.Delegates;

namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            ILibraryEngine libraryEngine = new LibraryEngine();

            List<Book> books = new List<Book>
            {
                new Book("111", "C# Basics",
                    new string[] { "Ali", "Sara" },
                    new DateTime(2020, 5, 1), 150),

                new Book("222", "Advanced C#",
                    new string[] { "Ahmed" },
                    new DateTime(2022, 8, 10), 250),

                new Book("333", "LINQ in Action",
                    new string[] { "Mona", "Khaled" },
                    new DateTime(2019, 3, 15), 200)
            };

            Console.WriteLine("=== 1. Using Custom Delegate with Method Reference (Title) ===");
            BookFunc titleFunc = bookService.GetTitle;
            libraryEngine.ProcessBooks(books, titleFunc);

            Console.WriteLine("\n=== 2. Using Func<Book, string> with Authors ===");
            Func<Book, string> authorsFunc = bookService.GetAuthors;
            foreach (var book in books)
                Console.WriteLine(authorsFunc(book));

            Console.WriteLine("\n=== 3. Using Anonymous Method (ISBN) ===");
            BookFunc isbnFunc = delegate (Book b)
            {
                return $"ISBN: {b.ISBN}";
            };
            libraryEngine.ProcessBooks(books, isbnFunc);

            Console.WriteLine("\n=== 4. Using Lambda Expression (Publication Date) ===");
            BookFunc dateFunc = b => $"Published: {b.PublicationDate:yyyy-MM-dd}";
            libraryEngine.ProcessBooks(books, dateFunc);

            Console.WriteLine("\n=== 5. Using Lambda Expression (Price) ===");
            BookFunc priceFunc = b => $"Price: {b.Price:C}";
            libraryEngine.ProcessBooks(books, priceFunc);

            Console.WriteLine("\n=== 6. Using Lambda Expression (Full Book Info) ===");
            BookFunc fullInfoFunc = b => b.ToString();
            libraryEngine.ProcessBooks(books, fullInfoFunc);

            Console.WriteLine("\n=== 7. Inline Lambda (Custom Formatting) ===");
            libraryEngine.ProcessBooks(books, b => $"{b.Title} ({b.PublicationDate.Year}) - {string.Join(", ", b.Authors)}");
        }
    }
}
