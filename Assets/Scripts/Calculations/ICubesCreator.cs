using System.Collections.Generic;
using UnityEngine;

public interface ICubesCreator
{
    public void Create(Color color, Matrix4x4[] matrices, Transform parent);
}