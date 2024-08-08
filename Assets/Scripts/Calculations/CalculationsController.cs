using UniRx;
using UnityEngine;
using Zenject;

public class CalculationsController : MonoBehaviour
{
    private readonly string _filePath = @"C:\Repos\Ceramic3d-TZ\Assets\JsonFiles\Result\offsets.json";

    [SerializeField] private TextAsset _modelJson;
    [SerializeField] private TextAsset _spaceJson;

    private IMatrixOffsetsFinder _offsetsFinder;
    private IOffsetsVisualizer _offsetsVisualizer;
    private IMatricesRenderer _matricesRenderer;
    private IMatrixJsonConvert _matrixJsonConvert;

    private void Start()
    {
        var modelMatrices = _matrixJsonConvert.GetMatrices(_modelJson.text);
        var spaceMatrices = _matrixJsonConvert.GetMatrices(_spaceJson.text);


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
        _matrixJsonConvert.ExportOffsetsToJson(_filePath, offsets);
    }
}