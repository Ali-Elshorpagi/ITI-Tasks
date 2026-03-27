namespace Task01
{
    internal static class Helper
    {
        public static Point3D ReadPointFromUser(string name)
        {

            int x, y, z;

            Console.WriteLine($"\nEnter coordinates for {name}:");

            Console.Write("X: ");
            while (!int.TryParse(Console.ReadLine(), out x))
                Console.Write("Invalid input. Enter X again: ");

            Console.Write("Y: ");
            while (!int.TryParse(Console.ReadLine(), out y))
                Console.Write("Invalid input. Enter Y again: ");

            Console.Write("Z: ");
            while (!int.TryParse(Console.ReadLine(), out z))
                Console.Write("Invalid input. Enter Z again: ");

            return new Point3D(x, y, z);
        }
    }
}
