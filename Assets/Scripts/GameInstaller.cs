using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<LevelBricksSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreSystem>().FromComponentInHierarchy().AsSingle();
    }
}