using DrebotGS.Core.Loading;
using DrebotGS.Factories;
using DrebotGS.Services;
using DrebotGS.StateMachine;
using Zenject;

namespace DrebotGS.Core
{
  public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
  {
    Contexts _contexts;

    public override void InstallBindings()
    {
      BindContext();
      BindFactories();
      BindSerices();
      BindLoadingProviders();
      BindStateMachine();
      BindHelpers();
    }

    private void BindContext()
    {
      _contexts = Contexts.sharedInstance;
      Container.Bind<Contexts>().FromInstance(_contexts);
    }

    private void BindFactories()
    {
      Container.Bind<LoadingProvidersFactory>().AsSingle();
    }

    private void BindSerices()
    {
      Container.Bind<SceneLoaderService>().AsSingle();
      Container.Bind<ILoadService>().To<UnityAddressablesLoadService>().AsSingle();
    }

    private void BindLoadingProviders()
    {
      Container.Bind<StandardLoadingScreenProvider>().AsSingle();
    }

    private void BindStateMachine()
    {
      Container.Bind<GameStateMachine>().AsSingle();
    }

    private void BindHelpers()
    {
      Container.Bind<LoadingSceneHelper>().AsSingle().NonLazy();
    }
  }
}