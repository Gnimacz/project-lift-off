using System;

namespace GXPEngine.Core
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString()
        {
            return "[Vector2 " + x + ", " + y + "]";
        }

        //my additions
        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }



        public Vector2 Normalized()
        {
            if (this.Length() > 0)
            {
                float length = Length();
                return new Vector2(x / length, y / length);
            }
            return new Vector2(0, 0);
        }

        public float DistanceTo(Vector2 other)
        {
            return (float)Math.Sqrt(Math.Pow(other.x - x, 2) + Math.Pow(other.y - y, 2));
        }

        public void CapLength(float maxLength)
        {
            if (this.Length() > maxLength)
            {
                Vector2 caped = this.Normalized() * maxLength;
                x = caped.x;
                y = caped.y;
            }
        }

        //operator overloads
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
    }
}