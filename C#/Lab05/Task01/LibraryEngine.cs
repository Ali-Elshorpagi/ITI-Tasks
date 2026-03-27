using System;
using System.Collections.Generic;
using System.Text;
using static Task01.Delegates;

namespace Task01
{
    internal class LibraryEngine : ILibraryEngine
    {
        public void ProcessBooks(List<Book> bList, BookFunc fPtr)
        {
            foreach (Book B in bList)
                Console.WriteLine(fPtr(B));
        }
    }
}
