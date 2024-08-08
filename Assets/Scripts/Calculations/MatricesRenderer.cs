using UnityEngine;
using Zenject;

public class MatricesRenderer : MonoBehaviour
{
    [SerializeField] private OffsetsVisualizer _modelMover;
    [SerializeField] private Transform _modelContainer;
    [SerializeField] private Transform _spaceContainer;
    [SerializeField] private float _delay;

    [Inject]
    private ICubesCreator _cubesCreator;

    public void Render(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        _cubesCreator.Create(Color.red, modelMatrices, _modelContainer);
        _cubesCreator.Create(Color.blue, spaceMatrices, _spaceContainer);
    }
}