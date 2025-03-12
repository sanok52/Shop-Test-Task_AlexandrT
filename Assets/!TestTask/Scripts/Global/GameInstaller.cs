using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMoveHandler>().To<JoystickConroller>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IRotateHandler>().To<TouchMoveController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IClickHandler>().To<ClickOnScreen>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerGrab>().FromComponentInHierarchy().AsSingle();
    }
}
