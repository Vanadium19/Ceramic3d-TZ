using UnityEngine;
using Utils;

public class GameObjectCreator : MonoBehaviour, IGameObjectCreator
{
    public void CreateObjects(GameObject prefab, Matrix4x4[] matrices, Transform parent)
    {
        foreach (var matrix in matrices)
        {
            var cube = Instantiate(prefab, matrix.GetPosition(), matrix.rotation, parent);

            cube.transform.localScale = matrix.GetScale();
        }
    }
}