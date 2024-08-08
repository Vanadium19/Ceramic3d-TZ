using System.Collections.Generic;
using UnityEngine;

public interface IMatrixOffsetsFinder
{
    public List<Matrix4x4> FindOffsets(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices);
}