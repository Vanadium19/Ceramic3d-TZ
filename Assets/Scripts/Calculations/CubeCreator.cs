using UnityEngine;
using Zenject;

[RequireComponent(typeof(JsonReader))]
public class CubeCreator : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [Inject]
    private IGameObjectCreator _gameObjectCreator;
    private JsonReader _jsonReader;

    private void Awake()
    {
        _jsonReader = GetComponent<JsonReader>();
    }

    private void Start()
    {
        Matrix4x4[] matrices = _jsonReader.GetMatrices();

        _gameObjectCreator.CreateObjects(_cubePrefab, matrices, transform);
    }
}