namespace Task01
{
    internal class Point3D : IComparable<Point3D>, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Point3D(int _x, int _y, int _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
        public Point3D(int _x, int _y) : this(_x, _y, 0) { }
        public Point3D(int _x) : this(_x, 0, 0) { }
        public Point3D() : this(0, 0, 0) { }
        public Point3D(Point3D pt) : this(pt.X, pt.Y, pt.Z) { }
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
        public static explicit operator string(Point3D p)
        {
            return p?.ToString() ?? string.Empty;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Point3D p)
                return X == p.X && Y == p.Y && Z == p.Z;

            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
        public object Clone()
        {
            return new Point3D(this);
        }
        public int CompareTo(Point3D? other)
        {
            if (other is null)
                return 1;

            if (X != other.X)
                return X.CompareTo(other.X);

            if (Y != other.Y)
                return Y.CompareTo(other.Y);

            return Z.CompareTo(other.Z);
        }
        public static bool operator ==(Point3D p1, Point3D p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (p1 is null || p2 is null)
                return false;

            return p1.Equals(p2);
        }
        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !(p1 == p2);
        }
    }
}
