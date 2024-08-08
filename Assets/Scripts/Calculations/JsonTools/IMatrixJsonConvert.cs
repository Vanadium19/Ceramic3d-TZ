using UnityEngine;

public interface IMatrixJsonConvert
{
    public Matrix4x4[] GetMatrices(string json);

    public void ExportOffsetsToJson(string path, Matrix4x4[] matrices);
}