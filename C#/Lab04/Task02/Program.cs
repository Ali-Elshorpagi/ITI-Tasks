namespace Task02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestMyList();
            Console.WriteLine("\n\n--------------------------------------------------------\n\n");
            TestMyListWithComplex();
        }
        static void TestMyListWithComplex()
        {
            MyList<Complex> list = new MyList<Complex>();

            Console.WriteLine("Adding complex numbers...");
            list.Add(new Complex(1, 2));
            list.Add(new Complex(3, -4));
            list.Add(new Complex(5, 6));
            list.Add(new Complex(1, 2)); // duplicate value

            Console.WriteLine($"Count after add: {list.Count}");

            Console.WriteLine("\nIndex access:");
            Console.WriteLine(list[0]); // 1 + 2i
            Console.WriteLine(list[1]); // 3 - 4i

            list[2] = new Complex(10, 10);
            Console.WriteLine($"After setting index 2: {list[2]}");

            Console.WriteLine("\nForeach iteration:");
            foreach (var c in list)
                Console.WriteLine(c);

            Console.WriteLine("\nRemoving Complex(1, 2)...");
            bool removed = list.Remove(new Complex(1, 2));
            Console.WriteLine($"Removed: {removed}");
            Console.WriteLine($"Count after remove: {list.Count}");

            Console.WriteLine("\nList after removal:");
            foreach (var c in list)
                Console.WriteLine(c);

            Console.WriteLine("\nTrying to remove non-existing Complex(100, 100)...");
            Console.WriteLine(list.Remove(new Complex(100, 100))); // false

            Console.WriteLine("\nFind first Complex with Real == 1:");
            Complex found = list.Find(c => c.Real == 1);
            Console.WriteLine(found);

            Console.WriteLine("\nFind all Complex numbers with real > 5:");
            MyList<Complex> largeOnes = list.FindAll(c => c.Real > 5);

            foreach (var c in largeOnes)
                Console.WriteLine(c);
        }
        static void TestMyList()
        {
            MyList<int> list = new MyList<int>();

            Console.WriteLine("Adding items...");
            for (int i = 1; i < 11; i++)
                list.Add(i * 10);

            Console.WriteLine($"Count after add: {list.Count}");

            Console.WriteLine("\nIndex access:");
            Console.WriteLine(list[0]);   // 10
            Console.WriteLine(list[5]);   // 60

            list[1] = 999;
            Console.WriteLine($"After setting index 1: {list[1]}");

            Console.WriteLine("\nForeach iteration:");
            foreach (var item in list)
                Console.Write(item + " ");

            Console.WriteLine();

            Console.WriteLine("\nRemoving item 999...");
            bool removed = list.Remove(999);
            Console.WriteLine($"Removed: {removed}");
            Console.WriteLine($"Count after remove: {list.Count}");

            Console.WriteLine("\nList after removal:");
            foreach (var item in list)
                Console.Write(item + " ");

            Console.WriteLine();

            Console.WriteLine("\nTrying to remove non-existing item (1234)...");
            Console.WriteLine(list.Remove(1234)); // false

            Console.WriteLine("\nFind first item > 50:");
            int found = list.Find(x => x > 50);
            Console.WriteLine(found);

            Console.WriteLine("\nFind all items divisible by 20:");
            MyList<int> divisibleBy20 = list.FindAll(x => x % 20 == 0);

            foreach (var item in divisibleBy20)
                Console.Write(item + " ");

            Console.WriteLine();

        }
    }
}
