using DrebotGS.Core.Loading;
using Zenject;

namespace DrebotGS.Core
{
  public class BootstrapInstaller : MonoInstaller
  {
    Contexts _contexts;

    public override void InstallBindings()
    {
      BindContext();
      SignalBusInstall();
      DeclareSignals();
      BindServises();
      BindLoadingProviders();
      BindLoadOperators();
    }

    private void BindContext()
    {
      _contexts = Contexts.sharedInstance;
      Container.Bind<Contexts>().FromInstance(_contexts);
    }

    private void SignalBusInstall()
    { SignalBusInstaller.Install(Container); }

    private void DeclareSignals()
    {
      //Container.DeclareSignal<SomeSignal>();
    }

    private void BindServises()
    {
      //Container.Bind<ILogService>().To<UnityLogServise>().AsSingle();
      //Container.Bind<IInputService>().To<UnityOldInputService>().AsSingle();
      //Container.Bind<ILoadService>().To<UnityLoadService>().AsSingle();
    }

    private void BindLoadingProviders()
    {
      Container.BindInterfacesAndSelfTo<LoadingSceneProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingScreenProvider>().AsSingle();
    }

    private void BindLoadOperators()
    {
      Container.BindInterfacesAndSelfTo<LoadIntroSceneOperation>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadTitileSceneOperation>().AsSingle();
    }
  }
}