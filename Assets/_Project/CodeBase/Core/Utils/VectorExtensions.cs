

using System;
using UnityEngine;

namespace _Project.CodeBase.Core.Utils
{
    public static class Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        
        public static Type[] ToArray(this Type type) =>
            new[] { type };
    }
}
