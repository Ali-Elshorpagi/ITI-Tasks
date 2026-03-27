using static Task01.Delegates;

namespace Task01
{
    internal interface ILibraryEngine
    {
        public void ProcessBooks(List<Book> bList, BookFunc fPtr);
    }
}
