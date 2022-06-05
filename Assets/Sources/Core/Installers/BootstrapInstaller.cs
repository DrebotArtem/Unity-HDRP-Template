using DrebotGS.Core.Loading;
using Zenject;

namespace DrebotGS.Core
{
  public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
  {
    Contexts _contexts;

    public override void InstallBindings()
    {
      BindContext();
      BindLoadingProviders();
      BindHelpers();
    }

    private void BindContext()
    {
      _contexts = Contexts.sharedInstance;
      Container.Bind<Contexts>().FromInstance(_contexts);
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