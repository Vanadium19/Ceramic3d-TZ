using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class MatrixJsonConvert : IMatrixJsonConvert
{
    public void ExportOffsetsToJson(string path, Matrix4x4[] matrices)
    {
        string json = JsonConvert.SerializeObject(matrices, Formatting.Indented, new MatrixJsonConverter());

        File.WriteAllText(path, json);
    }

    public Matrix4x4[] GetMatrices(string json)
    {
        return JsonConvert.DeserializeObject<Matrix4x4[]>(json);
    }
}