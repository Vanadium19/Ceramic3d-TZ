using System.Collections.Generic;
using UnityEngine;

public interface ICubesCreator
{
    public List<Transform> Create(Color color, Matrix4x4[] matrices);
}