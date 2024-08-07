using UnityEngine;

namespace Utils
{
    public static class MatrixExtension
    {
        private static readonly int _matrixElementsCount = 16;

        public static Vector3 GetScale(this Matrix4x4 matrix)
        {
            Vector3 scale;

            scale.x = matrix.GetColumn(0).magnitude;
            scale.y = matrix.GetColumn(1).magnitude;
            scale.z = matrix.GetColumn(2).magnitude;

            return scale;
        }

        public static bool IsEqual(this Matrix4x4 matrix, Matrix4x4 comparableMatrix, float floatError = 0.001f)
        {
            for (int i = 0; i < _matrixElementsCount; i++)
            {
                if (Mathf.Abs(matrix[i] - comparableMatrix[i]) > floatError)
                {
                    return false;
                }
            }

            return true;
        }
    }
}