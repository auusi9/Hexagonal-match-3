namespace Utils
{
    public struct CustomPoint
    {
        public int x, y;

        public CustomPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public static CustomPoint operator +(CustomPoint a, CustomPoint b)
        {
            return new CustomPoint(a.x + b.x, a.y + b.y);
        }

        public static CustomPoint operator -(CustomPoint a, CustomPoint b)
        {
            return new CustomPoint(a.x - b.x, a.y - b.y);
        }

        public static CustomPoint operator *(CustomPoint a, CustomPoint b)
        {
            return new CustomPoint(a.x * b.x, a.y * b.y);
        }

        public static CustomPoint operator /(CustomPoint a, CustomPoint b)
        {
            return new CustomPoint(a.x / b.x, a.y / b.y);
        }

        public static CustomPoint operator -(CustomPoint a)
        {
            return new CustomPoint(-a.x, -a.y);
        }

        public static CustomPoint operator *(CustomPoint a, int d)
        {
            return new CustomPoint(a.x * d, a.y * d);
        }

        public static CustomPoint operator *(int d, CustomPoint a)
        {
            return new CustomPoint(a.x * d, a.y * d);
        }

        public static CustomPoint operator /(CustomPoint a, int d)
        {
            return new CustomPoint(a.x / d, a.y / d);
        }

        public static bool operator ==(CustomPoint a, CustomPoint b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }

        public static bool operator !=(CustomPoint a, CustomPoint b)
        {
            return !(a == b);
        }
        
        public bool Equals(CustomPoint other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CustomPoint other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
    }
}