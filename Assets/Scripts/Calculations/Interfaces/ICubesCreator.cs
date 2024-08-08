using UnityEngine;

namespace Calculations.Interfaces
{
    public interface ICubesCreator
    {
        public void Create(Transform prefab, Matrix4x4[] matrices, Transform parent);
    }
}