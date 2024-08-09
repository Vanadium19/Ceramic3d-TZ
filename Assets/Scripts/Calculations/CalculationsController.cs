using System.IO;
using Calculations.Interfaces;
using JsonTools;
using UniRx;
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


            Observable.Start(() => _offsetsFinder.FindOffsets(modelMatrices, spaceMatrices))
                .ObserveOnMainThread()
                .Do(offsets => _matricesRenderer.Render(modelMatrices, spaceMatrices))
                .Subscribe(offsets => OnOffsetsFound(offsets.ToArray()))
                .AddTo(this);
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

        private void OnOffsetsFound(Matrix4x4[] offsets)
        {
            _offsetsVisualizer.Visualize(offsets);

            if (File.Exists(_resultFilePath))
                _matrixJsonConvert.ExportOffsetsToJson(_resultFilePath, offsets);
        }
    }
}