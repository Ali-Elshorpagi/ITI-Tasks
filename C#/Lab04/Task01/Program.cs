namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestSwap();
        }
        static void TestSwap()
        {
            int x = 5, y = 10;

            Console.WriteLine($"Before swap: x = {x}, y = {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"After swap:  x = {x}, y = {y}");

            string a = "hello", b = "world";

            Console.WriteLine($"Before swap: a = {a}, b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"After swap:  a = {a}, b = {b}");
        }
        static void Swap<Type>(ref Type a, ref Type b)
        {
            Type tmp = a;
            a = b;
            b = tmp;
        }
    }
}
