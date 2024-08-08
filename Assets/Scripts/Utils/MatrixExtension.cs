using UnityEngine;

namespace Utils
{
    public static class MatrixExtension
    {
        private static readonly int _matrixElementsCount = 16;

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