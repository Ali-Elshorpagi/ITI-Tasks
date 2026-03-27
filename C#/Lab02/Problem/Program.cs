namespace Problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float budget = 183.23f, bagVolume = 64.11f;
            int people = 7, nPresents = 12;

            float[] presentVolume =
            {
                4.53f, 9.11f, 4.53f, 6.00f, 1.04f, 0.87f,
                2.57f, 19.45f, 65.59f, 14.14f, 16.66f, 13.53f
            };

            float[] presentPrice =
            {
                12.23f, 45.03f, 12.23f, 32.93f, 6.99f, 0.46f,
                7.34f, 65.98f, 152.13f, 7.23f, 10.00f, 25.25f
            };

            float result = PresentList(budget, bagVolume, people, nPresents, presentVolume, presentPrice);

            Console.WriteLine($"Maximum money spent: {result:F2}");
        }
        static float PresentList(float budget, float bagVolume, int people, int nPresents, float[] presentVolume, float[] presentPrice)
        {
            float[] packCost = new float[nPresents], packVolume = new float[nPresents];

            for (int i = 0; i < nPresents; ++i)
            {
                packCost[i] = presentPrice[i] * people;
                packVolume[i] = presentVolume[i] * people;
            }

            var memory = new Dictionary<string, float>(); 

            return Solve(0, budget, bagVolume, packCost, packVolume, memory);
        }
        static float Solve(int idx, float budget, float volume, float[] packCost, float[] packVolume, Dictionary<string, float> memory)
        {
            if (idx >= packCost.Length || budget <= 0f || volume <= 0f)
                return 0f;

            string key = $"{idx}|{budget:F2}|{volume:F2}";
            if (memory.ContainsKey(key))
                return memory[key];

            float best = Solve(idx + 1, budget, volume, packCost, packVolume, memory);

            if (budget >= packCost[idx] && volume >= packVolume[idx])
            {
                float take = packCost[idx] + Solve(idx, budget - packCost[idx], volume - packVolume[idx], packCost, packVolume, memory);
                best = Math.Max(best, take);
            }

            memory[key] = best;

            return best;
        }
    }
}