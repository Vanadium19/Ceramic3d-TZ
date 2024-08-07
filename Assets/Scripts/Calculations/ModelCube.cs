using UnityEngine;

public class ModelCube
{
    private Transform _transform;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _lastPosition;
    private Quaternion _lastRotation;

    public ModelCube(Transform transform)
    {
        _transform = transform;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void Move(Matrix4x4 offset, float lerpFactor)
    {
        _transform.position = Vector3.Lerp(_startPosition, offset.MultiplyPoint3x4(_startPosition), lerpFactor);
        _transform.rotation = Quaternion.Lerp(_startRotation, offset.rotation * _startRotation, lerpFactor);

        _lastPosition = _transform.position;
        _lastRotation = _transform.rotation;
    }

    public void Reset(float lerpFactor)
    {
        _transform.position = Vector3.Lerp(_lastPosition, _startPosition, lerpFactor);
        _transform.rotation = Quaternion.Lerp(_lastRotation, _startRotation, lerpFactor);
    }
}