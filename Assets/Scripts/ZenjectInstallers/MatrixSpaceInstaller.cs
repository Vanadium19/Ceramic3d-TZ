using Calculations;
using Calculations.Interfaces;
using JsonTools;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class MatrixSpaceInstaller : MonoInstaller
    {
        [SerializeField] private MatricesRenderer _matricesRenderer;
        [SerializeField] private OffsetsVisualizer _offsetsVisualizer;

        public override void InstallBindings()
        {
            Container.Bind<IMatrixOffsetsFinder>().To<BurstMatrixOffsetsFinder>().AsSingle();
            Container.Bind<IMatrixJsonConvert>().To<MatrixJsonConvert>().AsSingle();
            Container.Bind<ICubesCreator>().To<CubesCreator>().AsSingle();
            Container.Bind<IMatricesRenderer>().FromInstance(_matricesRenderer);
            Container.Bind<IOffsetsVisualizer>().FromInstance(_offsetsVisualizer);
        }
    }
}