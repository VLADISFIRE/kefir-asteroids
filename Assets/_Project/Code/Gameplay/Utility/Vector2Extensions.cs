using UnityEngine;

namespace Gameplay.Utility
{
    public static class Vector2Extensions
    {
        public static Vector2 Left(this Vector2 vector)
        {
            return new Vector2(-vector.y, vector.x);
        }

        public static Vector2 Right(this Vector2 vector)
        {
            return new Vector2(vector.y, -vector.x);
        }

        public static Quaternion ToQuaternion(this Vector2 vector)
        {
            var angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static ref Vector2 Rotate(this ref Vector2 vector, float rad)
        {
            vector = new Vector2(vector.x * Mathf.Cos(rad) - vector.y * Mathf.Sin(rad), vector.x * Mathf.Sin(rad) + vector.y * Mathf.Cos(rad));
            return ref vector;
        }
    }
}