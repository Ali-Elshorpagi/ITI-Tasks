namespace Task02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //           0               1
            int[] arr = { 1, 2, 1, 4, 5, 1 };
            int max = int.MinValue;
            for (int i = 0; i < arr.Length; ++i)
            {
                if (max > arr.Length - i)
                    break;
                for (int j = arr.Length - 1; j > i; --j)
                {
                    if(arr[i] == arr[j])
                    {
                        int dist = j - i - 1;
                        if (dist > max)
                            max = dist;
                        break;
                    }
                }
            }
            Console.WriteLine($"The max distance between two similar number is {max}");
        }
    }
}
