using UnityEngine;
using Zenject;

public class MatricesRenderer : MonoBehaviour, IMatricesRenderer
{
    [SerializeField] private Transform _modelCubePrefab;
    [SerializeField] private Transform _spaceCubePrefab;

    [SerializeField] private Transform _modelContainer;
    [SerializeField] private Transform _spaceContainer;

    [Inject]
    private ICubesCreator _cubesCreator;

    public void Render(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        _cubesCreator.Create(_modelCubePrefab, modelMatrices, _modelContainer);
        _cubesCreator.Create(_spaceCubePrefab, spaceMatrices, _spaceContainer);
    }
}