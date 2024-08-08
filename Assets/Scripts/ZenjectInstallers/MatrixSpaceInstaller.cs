using UnityEngine;
using Zenject;

public class MatrixSpaceInstaller : MonoInstaller
{
    [SerializeField] private OffsetsVisualizer _offsetsVisualizer;

    public override void InstallBindings()
    {
        Container.Bind<IMatrixJsonConvert>().To<MatrixJsonConvert>().AsSingle();
        Container.Bind<ICubesCreator>().To<CubesCreator>().AsSingle();
        Container.Bind<IOffsetsVisualizer>().FromInstance(_offsetsVisualizer);
    }
}