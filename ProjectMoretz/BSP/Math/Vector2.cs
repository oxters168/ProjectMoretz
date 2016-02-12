using System;

namespace OxMath
{
    public struct Vector2
    {
        public float x, y;
        public Vector2 normalized { get { return new Vector2(x / Magnitude(), y / Magnitude()); } private set { } }
        public bool isZero { get { return (x == 0 && y == 0); } private set { } }
        public static Vector2 zero { get { return new Vector2(0, 0); } private set { } }
        public static Vector2 one { get { return new Vector2(1, 1); } private set { } }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        #region Operator overloading
        static public Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        }
        static public Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);
        }
        static public Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        static public Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }

        static public bool operator ==(Vector2 vector1, Vector2 vector2)
        {
            if (vector1.x == vector2.x && vector1.y == vector2.y)
                return true;

            return false;
        }
        static public bool operator !=(Vector2 vector1, Vector2 vector2)
        {
            if (vector1.x != vector2.x || vector1.y != vector2.y)
                return true;

            return false;
        }
        #endregion

        #region object overrides
        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return this == (Vector2)obj;
            }

            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
        #endregion

        public float Magnitude()
        {
            return (float)Math.Sqrt((x * x) + (y * y));
        }
        public void Normalize()
        {
            float length = Magnitude();
            x /= length;
            y /= length;
        }

        public static float Dot(Vector2 vector1, Vector2 vector2)
        {
            return ((vector1.x * vector2.x) + (vector1.y * vector2.y));
        }
        public static float Distance(Vector2 vector1, Vector2 vector2)
        {
            return (float)Math.Sqrt(Math.Pow(vector2.x - vector1.x, 2) + Math.Pow(vector2.y - vector1.y, 2));
        }
    }
}