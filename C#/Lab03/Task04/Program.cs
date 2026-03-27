namespace Task04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Duration Class ===\n");

            TestConstructorsAndToString();
            TestAdditionOperators();
            TestIncrementDecrement();
            TestComparisonOperators();
            TestTrueFalseOperator();
            TestDateTimeCasting();

            Console.WriteLine("\n=== Ending ===");
        }
        static void TestConstructorsAndToString()
        {
            Console.WriteLine("\nTesting Constructors & ToString:");

            Duration d1 = new Duration(1, 10, 15);
            Console.WriteLine(d1);

            Duration d2 = new Duration(3600);
            Console.WriteLine(d2);

            Duration d3 = new Duration(7800);
            Console.WriteLine(d3);

            Duration d4 = new Duration(666);
            Console.WriteLine(d4);

            Console.WriteLine();
        }
        static void TestAdditionOperators()
        {
            Console.WriteLine("\nTesting + Operators:");

            Duration d1 = new Duration(1, 0, 0);
            Duration d2 = new Duration(7800);

            Duration d3 = d1 + d2;
            Console.WriteLine($"D1 + D2 = {d3}");

            d3 = d1 + 666;
            Console.WriteLine($"D1 + 666 = {d3}");

            d3 = 666 + d2;
            Console.WriteLine($"666 + D2 = {d3}");

            Console.WriteLine();
        }
        static void TestIncrementDecrement()
        {
            Console.WriteLine("\nTesting ++ and --:");

            Duration d1 = new Duration(1, 10, 0);
            Duration d2 = new Duration(120);

            Duration d3 = d1++;
            Console.WriteLine($"D1++ = {d3}");

            d3 = --d2;
            Console.WriteLine($"--D2 = {d3}");

            Console.WriteLine();
        }
        static void TestComparisonOperators()
        {
            Console.WriteLine("\nTesting Comparison Operators:");

            Duration d1 = new Duration(3600);
            Duration d2 = new Duration(1800);

            if (d1 > d2)
                Console.WriteLine("D1 > D2");

            if (d1 >= d2)
                Console.WriteLine("D1 >= D2");

            if (d2 < d1)
                Console.WriteLine("D2 < D1");

            if (d2 <= d1)
                Console.WriteLine("D2 <= D1");

            Console.WriteLine();
        }
        static void TestTrueFalseOperator()
        {
            Console.WriteLine("\nTesting true / false Operator:");

            Duration d1 = new Duration(10);
            Duration d2 = new Duration(0);

            if (d1)
                Console.WriteLine("D1 is NOT zero");

            if (!d2)
                Console.WriteLine("D2 is zero");

            Console.WriteLine();
        }
        static void TestDateTimeCasting()
        {
            Console.WriteLine("\nTesting DateTime Casting:");

            Duration d = new Duration(1, 30, 0);
            DateTime dt = (DateTime)d;

            Console.WriteLine($"Duration: {d}");
            Console.WriteLine($"DateTime: {dt}");

            Console.WriteLine();
        }
    }
}
