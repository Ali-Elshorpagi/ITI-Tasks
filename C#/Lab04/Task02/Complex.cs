namespace Task02
{
    internal class Complex
    {
        public int Real { get; private set; }
        public int Img { get; private set; }
        public Complex()
        {
            Real = 0;
            Img = 0;
        }
        public Complex(int real, int img)
        {
            Real = real;
            Img = img;
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            
            if (obj is not Complex other)
                return false;

            return Real.Equals(other.Real) && Img.Equals(other.Img);
        }
        public override int GetHashCode() => HashCode.Combine(Real, Img);
        public override string ToString()
        {
            if (Img > 0)
                return $"{Real} + {Img}i";
            else
                return $"{Real} - {Math.Abs(Img)}i";
        }
    }
}
