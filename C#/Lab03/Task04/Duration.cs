namespace Task04
{
    internal class Duration
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        private int ToTotalSeconds() => Hours * 3600 + Minutes * 60 + Seconds;
        private void Normalize(int totalSeconds)
        {
            if (totalSeconds < 0)
                totalSeconds = 0;

            Hours = totalSeconds / 3600;
            totalSeconds %= 3600;

            Minutes = totalSeconds / 60;
            Seconds = totalSeconds % 60;
        }
        public Duration(int hours, int minutes, int seconds)
        {
            Normalize(hours * 3600 + minutes * 60 + seconds);
        }
        public Duration(int totalSeconds)
        {
            Normalize(totalSeconds);
        }
        public override string ToString()
        {
            if (Hours > 0)
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
            else if (Minutes > 0)
                return $"Minutes :{Minutes}, Seconds :{Seconds}";
            else
                return $"Seconds :{Seconds}";
        }
        public override bool Equals(object? obj)
        {
            // check nullable
            // use getType() first
            if (obj is Duration d)
                return ToTotalSeconds() == d.ToTotalSeconds();

            return false;
        }
        public override int GetHashCode() => ToTotalSeconds().GetHashCode();
        public static Duration operator +(Duration d1, Duration d2) => new Duration(d1.ToTotalSeconds() + d2.ToTotalSeconds());
        public static Duration operator +(Duration d, int seconds) => new Duration(d.ToTotalSeconds() + seconds);
        public static Duration operator +(int seconds, Duration d) => new Duration(seconds + d.ToTotalSeconds());
        public static Duration operator ++(Duration d) => new Duration(d.ToTotalSeconds() + 60);
        public static Duration operator --(Duration d) => new Duration(d.ToTotalSeconds() - 60);
        public static bool operator >(Duration d1, Duration d2) => d1.ToTotalSeconds() > d2.ToTotalSeconds();
        public static bool operator <(Duration d1, Duration d2) => d1.ToTotalSeconds() < d2.ToTotalSeconds();
        public static bool operator >=(Duration d1, Duration d2) => d1.ToTotalSeconds() >= d2.ToTotalSeconds();
        public static bool operator <=(Duration d1, Duration d2) => d1.ToTotalSeconds() <= d2.ToTotalSeconds();
        public static bool operator true(Duration d) => d.ToTotalSeconds() > 0;
        public static bool operator false(Duration d) => d.ToTotalSeconds() <= 0;
        public static bool operator !(Duration d) => d.ToTotalSeconds() <= 0;
        public static explicit operator DateTime(Duration d) => DateTime.Today.AddSeconds(d.ToTotalSeconds());
    }
}
