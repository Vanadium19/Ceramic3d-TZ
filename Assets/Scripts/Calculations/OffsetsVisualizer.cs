using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class OffsetsVisualizer : MonoBehaviour, IOffsetsVisualizer
{
    [SerializeField] private float _delay;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Visualize(Matrix4x4[] offsets)
    {
        IObservable<Unit> observable = Observable.ReturnUnit();

        foreach (var offset in offsets)
        {
            observable = observable
                .SelectMany(() => MoveModel(offset.GetPosition(), offset.rotation))
                .SelectMany(unit => Observable.Timer(TimeSpan.FromSeconds(_delay)))
                .SelectMany(() => MoveModel(Vector3.zero, Quaternion.identity));
        }

        observable.Subscribe().AddTo(this);
    }

    private IEnumerator MoveModel(Vector3 position, Quaternion rotation)
    {
        float elapsedTime = 0;
        float lerpFactor = 0;

        Vector3 startPosition = _transform.position;
        Quaternion startRotation = _transform.rotation;

        while (lerpFactor < 1)
        {
            elapsedTime += Time.deltaTime;
            lerpFactor = elapsedTime / _delay;
            _transform.position = Vector3.Lerp(startPosition, position, lerpFactor);
            _transform.rotation = Quaternion.Lerp(startRotation, rotation, lerpFactor);
            yield return null;
        }
    }
}