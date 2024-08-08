using System.Collections.Generic;
using UnityEngine;

namespace Calculations.Interfaces
{
    public interface IMatrixOffsetsFinder
    {
        public List<Matrix4x4> FindOffsets(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices);
    }
}