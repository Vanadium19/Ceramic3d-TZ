using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

[RequireComponent(typeof(MatricesRenderer))]
public class MatrixOffsetsFinder : MonoBehaviour
{
    private readonly string _filePath = @"C:\Repos\Ceramic3d-TZ\Assets\JsonFiles\Result\offsets.json";
    private readonly int _firstArrayIndex = 0;

    [SerializeField] private TextAsset _modelJson;
    [SerializeField] private TextAsset _spaceJson;

    [Inject]
    private IMatrixJsonConvert _matrixJsonConvert;
    private MatricesRenderer _matricesRenderer;

    private void Awake()
    {
        _matricesRenderer = GetComponent<MatricesRenderer>();
    }

    private void Start()
    {
        var modelMatrices = _matrixJsonConvert.GetMatrices(_modelJson.text);
        var spaceMatrices = _matrixJsonConvert.GetMatrices(_spaceJson.text);

        _matricesRenderer.Render(modelMatrices, spaceMatrices);

        Matrix4x4[] offsets = FindOffsets(modelMatrices, spaceMatrices).ToArray();

        _matricesRenderer.RenderOffsets(offsets);

        _matrixJsonConvert.ExportOffsetsToJson(_filePath, offsets);
    }

    private List<Matrix4x4> FindOffsets(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        Matrix4x4 offset;
        Matrix4x4 modelMatrix = modelMatrices[_firstArrayIndex];        
        List<Matrix4x4> offsets = new List<Matrix4x4>();

        foreach (var spaceMatrix in spaceMatrices)
        {
            offset = spaceMatrix * modelMatrix.inverse;

            //Quaternion rotation = modelMatrix.rotation * spaceMatrix.rotation;
            //Vector3 position = spaceMatrix.GetPosition() - modelMatrix.GetPosition();
            //offset = Matrix4x4.TRS(position, rotation, Vector3.one);

            if (IsMatchWithOffset(modelMatrices, spaceMatrices, offset))
                offsets.Add(offset);
        }

        return offsets;
    }

    private bool IsMatchWithOffset(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices, Matrix4x4 offset)
    {
        bool matchFound = false;
        Matrix4x4 transformedMatrix;

        foreach (var modelMatrix in modelMatrices)
        {
            transformedMatrix = offset * modelMatrix;

            foreach (var spaceMatrix in spaceMatrices)
            {
                matchFound = transformedMatrix.IsEqual(spaceMatrix);

                if (matchFound)
                    break;
            }

            if (!matchFound)
                return false;
        }

        return true;
    }
}