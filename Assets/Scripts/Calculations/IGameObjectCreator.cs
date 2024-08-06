using UnityEngine;

public interface IGameObjectCreator
{
    public void CreateObjects(GameObject prefab, Matrix4x4[] matrices, Transform parent);
}