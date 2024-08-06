using Newtonsoft.Json;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    [SerializeField] private TextAsset _jsonFile;

    public Matrix4x4[] GetMatrices()
    {
        return JsonConvert.DeserializeObject<Matrix4x4[]>(_jsonFile.text);
    }
}