using UnityEngine;

namespace Utils
{
    public static class MatrixExtension
    {
        public static Vector3 GetScale(this Matrix4x4 matrix)
        {
            Vector3 scale;

            scale.x = matrix.GetColumn(0).magnitude;
            scale.y = matrix.GetColumn(1).magnitude;
            scale.z = matrix.GetColumn(2).magnitude;

            return scale;
        }
    }
}