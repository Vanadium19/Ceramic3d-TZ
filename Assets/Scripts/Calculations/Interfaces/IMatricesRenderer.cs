using UnityEngine;

namespace Calculations.Interfaces
{
    public interface IMatricesRenderer
    {
        public void Render(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices);
    }
}