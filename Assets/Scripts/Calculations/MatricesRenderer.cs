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

    public void Render(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        _cubesCreator.Create(Color.red, modelMatrices, _modelContainer);
        _cubesCreator.Create(Color.blue, spaceMatrices, _spaceContainer);
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
            moving = StartCoroutine(MoveCubes(offset.GetPosition(), offset.rotation));
            yield return moving;
            yield return delay;
            moving = StartCoroutine(MoveCubes(Vector3.zero, Quaternion.identity));
            yield return moving;
        }

        Debug.Log("Finish");
    }

    private IEnumerator MoveCubes(Vector3 position, Quaternion rotation)
    {
        float elapsedTime = 0;
        float lerpFactor = 0;

        Vector3 startPosition = _modelContainer.position;
        Quaternion startRotation = _modelContainer.rotation;

        while (lerpFactor < 1)
        {
            elapsedTime += Time.deltaTime;
            lerpFactor = elapsedTime / _delay;
            _modelContainer.position = Vector3.Lerp(startPosition, position, lerpFactor);
            _modelContainer.rotation = Quaternion.Lerp(startRotation, rotation, lerpFactor);
            yield return null;
        }
    }
}