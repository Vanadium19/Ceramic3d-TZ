using System.IO;
using Calculations.Interfaces;
using JsonTools;
using UnityEngine;
using Zenject;

namespace Calculations
{
    internal class CalculationsController : MonoBehaviour
    {
        [SerializeField] private TextAsset _modelJson;
        [SerializeField] private TextAsset _spaceJson;
        [SerializeField] private string _resultFilePath;

        private IMatrixOffsetsFinder _offsetsFinder;
        private IOffsetsVisualizer _offsetsVisualizer;
        private IMatricesRenderer _matricesRenderer;
        private IMatrixJsonConvert _matrixJsonConvert;

        private void Start()
        {
            Matrix4x4[] modelMatrices = _matrixJsonConvert.GetMatrices(_modelJson.text);
            Matrix4x4[] spaceMatrices = _matrixJsonConvert.GetMatrices(_spaceJson.text);

            Execute(modelMatrices, spaceMatrices);
        }

        [Inject]
        private void Construct(
            IMatrixOffsetsFinder matrixOffsetsFinder,
            IOffsetsVisualizer offsetsVisualizer,
            IMatricesRenderer matricesRenderer,
            IMatrixJsonConvert matrixJsonConvert)
        {
            _offsetsFinder = matrixOffsetsFinder;
            _offsetsVisualizer = offsetsVisualizer;
            _matricesRenderer = matricesRenderer;
            _matrixJsonConvert = matrixJsonConvert;
        }

        private void Execute(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
        {
            Matrix4x4[] offsets = _offsetsFinder.FindOffsets(modelMatrices, spaceMatrices).ToArray();

            _matricesRenderer.Render(modelMatrices, spaceMatrices);
            _offsetsVisualizer.Visualize(offsets);

            if (File.Exists(_resultFilePath))
                _matrixJsonConvert.ExportOffsetsToJson(_resultFilePath, offsets);
        }
    }
}