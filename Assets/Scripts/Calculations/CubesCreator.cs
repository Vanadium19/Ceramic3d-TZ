using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CubesCreator : ICubesCreator
{
    public void Create(Color color, Matrix4x4[] matrices, Transform parent)
    {
        foreach (var matrix in matrices)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

            cube.position = matrix.GetPosition();
            cube.rotation = matrix.rotation;
            cube.localScale = matrix.GetScale();
            cube.GetComponent<MeshRenderer>().material.color = color;

            cube.SetParent(parent);
        }
    }
}