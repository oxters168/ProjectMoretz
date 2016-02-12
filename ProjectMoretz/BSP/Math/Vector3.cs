using System;

namespace OxMath
{
    public struct Vector3
    {
        public float x, y, z;
        public Vector3 normalized { get { return new Vector3(x / Magnitude(), y / Magnitude(), z / Magnitude()); } private set { } }
        public bool isZero { get { return (x == 0 && y == 0 && z == 0); } private set { } }
        public static Vector3 zero { get { return new Vector3(0, 0, 0); } private set { } }
        public static Vector3 one { get { return new Vector3(1, 1, 1); } private set { } }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Operator overloading
        static public Vector3 operator +(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.x + vector2.x, vector1.y + vector2.y, vector1.z + vector2.z);
        }
        static public Vector3 operator -(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.x - vector2.x, vector1.y - vector2.y, vector1.z - vector2.z);
        }
        static public Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }
        static public Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
        }

        static public bool operator ==(Vector3 vector1, Vector3 vector2)
        {
            if (vector1.x == vector2.x && vector1.y == vector2.y && vector1.z == vector2.z)
                return true;

            return false;
        }
        static public bool operator !=(Vector3 vector1, Vector3 vector2)
        {
            if (vector1.x != vector2.x || vector1.y != vector2.y || vector1.z != vector2.z)
                return true;

            return false;
        }
        #endregion

        #region object overrides
        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return this == (Vector3)obj;
            }

            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }
        #endregion

        public float Magnitude()
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public void Normalize()
        {
            float length = Magnitude();
            x /= length;
            y /= length;
            z /= length;
        }

        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return ((vector1.x * vector2.x) + (vector1.y * vector2.y) + (vector1.z * vector2.z));
        }
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3((vector1.y * vector2.z) - (vector1.z * vector2.y), (vector1.z * vector2.x) - (vector1.x * vector2.z), (vector1.x * vector2.y) - (vector1.y * vector2.x));
        }
        public static float Distance(Vector3 vector1, Vector3 vector2)
        {
            return (float)Math.Sqrt(Math.Pow(vector2.x - vector1.x, 2) + Math.Pow(vector2.y - vector1.y, 2) + Math.Pow(vector2.z - vector1.z, 2));
        }
    }
}