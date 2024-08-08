using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MatrixOffsetsFinder : IMatrixOffsetsFinder
{
    private readonly int _firstArrayIndex = 0;   

    public List<Matrix4x4> FindOffsets(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        Matrix4x4 offset;
        Matrix4x4 modelMatrix = modelMatrices[_firstArrayIndex];        
        List<Matrix4x4> offsets = new List<Matrix4x4>();

        foreach (var spaceMatrix in spaceMatrices)
        {
            offset = spaceMatrix * modelMatrix.inverse;

            if (IsMatchWithOffset(modelMatrices, spaceMatrices, offset))
                offsets.Add(offset);
        }

        return offsets;
    }

    private bool IsMatchWithOffset(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices, Matrix4x4 offset)
    {
        bool matchFound = false;
        Matrix4x4 transformedMatrix;

        foreach (var modelMatrix in modelMatrices)
        {
            transformedMatrix = offset * modelMatrix;

            foreach (var spaceMatrix in spaceMatrices)
            {
                matchFound = transformedMatrix.IsEqual(spaceMatrix);

                if (matchFound)
                    break;
            }

            if (!matchFound)
                return false;
        }

        return true;
    }
}