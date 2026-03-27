using System.Diagnostics;

namespace Task03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            RunTestUsingString(sw);
            RunTestUsingModulas(sw);
            RunTestUsingFormula(sw);
        }
        private static void RunTestUsingString(Stopwatch sw)
        {
            sw.Restart();
            sw.Start();
            Console.Write($"\n=> Using String: {CountOneUsingString(99999999)}");
            sw.Stop();
            double time = sw.Elapsed.TotalSeconds;
            Console.WriteLine($"\t with time: {time} sec");
        }
        private static void RunTestUsingModulas(Stopwatch sw)
        {
            sw.Restart();
            sw.Start();
            Console.Write($"\n=> Using Modulas: {CountOneUsingModulas(99999999)}");
            sw.Stop();
            double time = sw.Elapsed.TotalSeconds;
            Console.WriteLine($"\t with time: {time} sec");
        }
        private static void RunTestUsingFormula(Stopwatch sw)
        {
            sw.Restart();
            sw.Start();
            Console.Write($"\n=> Using Formula: {CountOneUsingFormula(99999999)}");
            sw.Stop();
            double time = sw.Elapsed.TotalSeconds;
            Console.WriteLine($"\t with time: {time} sec");
        }
        private static long CountOneUsingString(int end) // O(N * log10(N))
        {
            long cnt = 0;
            for(int num = 1; num <= end; ++num)
            {
                cnt += num.ToString().Count('1');
                //foreach(char ch in num.ToString())
                //{
                //    if (ch == '1')
                //        ++cnt;
                //}
            }
            return cnt;
        }
        private static long CountOneUsingModulas(int end) // O(N * log10(N))
        {
            long cnt = 0;
            for (int num = 1; num <= end; ++num)
            {
                int tmp = num;
                while(tmp > 0)
                {
                    if (tmp % 10 == 1)
                        ++cnt;
                    tmp /= 10;
                }
            }
            return cnt;
        }
        private static long CountOneUsingFormula(int end) // O(log10(N)
        {
            byte digit = 1;
            long cnt = 0;
            for (long factor = 1; end / factor > 0; factor *= 10)
            {
                long lower = end % factor,
                 current = (end / factor) % 10,
                 higher = end / (factor * 10);

                if (current > digit)
                    cnt += (higher + 1) * factor;
                else if (current == digit)
                    cnt += (higher * factor) + lower + 1;
                else
                    cnt += higher * factor;
               
            }
            return cnt;
        }
    }
}
