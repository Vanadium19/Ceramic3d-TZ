using UnityEngine;
using Zenject;

public class MatrixSpaceInstaller : MonoInstaller
{    
    public override void InstallBindings()
    {
        Container.Bind<IMatrixJsonConvert>().To<MatrixJsonConvert>().AsSingle();
        Container.Bind<ICubesCreator>().To<CubesCreator>().AsSingle();
    }
}