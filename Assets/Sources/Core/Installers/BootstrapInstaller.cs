using DrebotGS.Core.Loading;
using DrebotGS.Services;
using Zenject;

namespace DrebotGS.Core
{
  public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
  {
    Contexts _contexts;

    public override void InstallBindings()
    {
      BindContext(); 
      BindSerices();
      BindLoadingProviders();
      BindHelpers();
    }

    private void BindContext()
    {
      _contexts = Contexts.sharedInstance;
      Container.Bind<Contexts>().FromInstance(_contexts);
    }

    private void BindSerices()
    {
      Container.Bind<ILoadService>().To<UnityAddressablesLoadService>().AsSingle();
    }

    private void BindLoadingProviders()
    {
      Container.Bind<StandardLoadingScreenProvider>().AsSingle();
    }

    private void BindHelpers()
    {
      Container.Bind<LoadingSceneHelper>().AsSingle().NonLazy();
    }
  }
}