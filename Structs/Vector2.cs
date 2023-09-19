namespace MyGame
{
    using System;

    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 Zero => new Vector2(0, 0);

        public static float Distance(Vector2 a, Vector2 b)
        {
            float newX = b.x - a.x;
            float newY = b.y - a.y;

            float length = (float)Math.Sqrt(newX * newX + newY * newY);
            return length;
        }

        public static float Magnitude(Vector2 a)
        {
            return (float)Math.Sqrt(a.x * a.x + a.y * a.y);
        }

        public static Vector2 Normalize(Vector2 a)
        {
            float magnitude = Vector2.Magnitude(a);
            Vector2 newVector = new Vector2(a.x / magnitude, a.y / magnitude);
            return newVector;
        }
    }
}