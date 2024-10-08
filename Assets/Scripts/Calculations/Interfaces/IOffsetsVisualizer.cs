using UnityEngine;

namespace Calculations.Interfaces
{
    public interface IOffsetsVisualizer
    {
        public void Visualize(Matrix4x4[] offsets);
    }
}