using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MatricesRenderer : MonoBehaviour
{
    [SerializeField] private Transform _modelContainer;
    [SerializeField] private Transform _spaceContainer;
    [SerializeField] private float _delay;

    [Inject]
    private ICubesCreator _cubesCreator;
    private List<ModelCube> _modelCubes = new List<ModelCube>();

    public void Render(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        var modelCubes = _cubesCreator.Create(Color.red, modelMatrices);

        foreach (var cube in modelCubes)
        {
            cube.SetParent(_modelContainer);
            _modelCubes.Add(new ModelCube(cube));
        }
        
        var spaceCubes = _cubesCreator.Create(Color.blue, spaceMatrices);

        foreach (var cube in spaceCubes)
        {
            cube.SetParent(_spaceContainer);
        }
    }

    public void RenderOffsets(Matrix4x4[] offsets)
    {
        StartCoroutine(StartMove(offsets));
    }

    private IEnumerator StartMove(Matrix4x4[] offsets)
    {
        var delay = new WaitForSeconds(_delay);
        Coroutine moving;

        foreach (var offset in offsets)
        {
            moving = StartCoroutine(MoveCubes(offset));
            yield return moving;
            yield return delay;
            moving = StartCoroutine(ResetCubes());
            yield return moving;
        }

        Debug.Log("Finish");
    }

    private IEnumerator MoveCubes(Matrix4x4 offset)
    {
        float elapsedTime = 0;
        float lerpFactor = 0;

        while (lerpFactor < 1)
        {
            elapsedTime += Time.deltaTime;
            lerpFactor = elapsedTime / _delay;

            foreach (var cube in _modelCubes)
            {
                cube.Move(offset, lerpFactor);
            }

            yield return null;
        }
    }

    private IEnumerator ResetCubes()
    {
        float elapsedTime = 0;
        float lerpFactor = 0;

        while (lerpFactor < 1)
        {
            elapsedTime += Time.deltaTime;
            lerpFactor = elapsedTime / _delay;

            foreach (var cube in _modelCubes)
            {
                cube.Reset(lerpFactor);
            }

            yield return null;
        }
    }
}