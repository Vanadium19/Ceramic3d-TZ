using UnityEngine;
using Zenject;

public class MatrixSpaceInstaller : MonoInstaller
{
    [SerializeField] private GameObjectCreator _gameObjectCreator;

    public override void InstallBindings()
    {
        Container.Bind<IGameObjectCreator>().FromInstance(_gameObjectCreator);
    }
}