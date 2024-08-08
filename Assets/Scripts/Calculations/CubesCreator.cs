using UnityEngine;


public class CubesCreator : ICubesCreator
{
    public void Create(Transform prefab, Matrix4x4[] matrices, Transform parent)
    {
        foreach (var matrix in matrices)
            Object.Instantiate(prefab, matrix.GetPosition(), matrix.rotation, parent);
    }
}