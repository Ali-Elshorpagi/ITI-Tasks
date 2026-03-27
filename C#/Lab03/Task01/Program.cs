namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Point3D Program ===\n");

            TestToStringAndCasting();

            Point3D p1 = Helper.ReadPointFromUser("P1");
            Point3D p2 = Helper.ReadPointFromUser("P2");
            Point3D p3 = Helper.ReadPointFromUser("P3");

            TestEquality(p1, p2);
            TestSorting(p1, p2, p3);
            TestCloning(p1);

            Console.WriteLine("\n=== Ending ===");
        }
        static void TestToStringAndCasting()
        {
            Console.WriteLine("\nTesting ToString & Casting:");

            Point3D p = new Point3D(10, 10, 10);

            Console.WriteLine(p);
            string s = (string)p;
            Console.WriteLine($"Casted string: {s}\n");
        }
        static void TestEquality(Point3D p1, Point3D p2)
        {
            Console.WriteLine("\nTesting Equality:");

            if (p1 == p2)
                Console.WriteLine("P1 == P2 (Equal)");
            else
                Console.WriteLine("P1 != P2 (Not Equal)");
        }
        static void TestSorting(Point3D p1, Point3D p2, Point3D p3)
        {
            Console.WriteLine("\nTesting Sorting (X then Y then Z):");

            Point3D[] points =
            {
                p1, p2, p3,
                new Point3D(5, 1, 0),
                new Point3D(1, 9, 3),
                new Point3D(1, 2, 3),
                new Point3D(0, 5, 5)
            };

            Array.Sort(points);

            foreach (Point3D p in points)
                Console.WriteLine(p);
        }
        static void TestCloning(Point3D original)
        {
            Console.WriteLine("\nTesting Cloning:");

            Point3D clone = (Point3D)original.Clone();

            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Clone   : {clone}");
        }
    }
}
