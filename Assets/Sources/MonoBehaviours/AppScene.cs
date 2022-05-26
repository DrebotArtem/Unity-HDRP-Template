using DrebotGS.Systems;
using UnityEngine;
using Zenject;

namespace DrebotGS
{
  public class AppScene : MonoBehaviour
  {
    private InjectableFeature _loadSystems;

    //Injects
    private DiContainer _diContainer;
    private Contexts _contexts;
    private LoadingSceneHelper _loadingSceneHelper;

    [Inject]
    public void Inject(
      DiContainer diContainer,
      Contexts contexts,
      LoadingSceneHelper loadingSceneHelper)
    {
      _diContainer = diContainer;
      _contexts = contexts;
      _loadingSceneHelper = loadingSceneHelper;
    }

    void Start()
    {
      CreateAndInitLoadSystems();

      // Just example load Intro scene throw loading screen
      _loadingSceneHelper.LoadIntroScene();
    }

    private void CreateAndInitLoadSystems()
    {
      _loadSystems = new InjectableFeature("LoadSystems");
      CreateLoadSystems(_contexts);
      _loadSystems.IncjectSelfAndChildren(_diContainer);
      _loadSystems.Initialize();

      void CreateLoadSystems(Contexts contexts)
      {
        _loadSystems.Add(new LoadSceneSystem(contexts));
        _loadSystems.Add(new ProcessLoadingOperationSystem(contexts));
        _loadSystems.Add(new UnloadProviderSystem(contexts));

        _loadSystems.Add(new DestroyDestroyedGameStateSystem(contexts));
      }
    }

    private void Update()
    {
      _loadSystems.Execute();
      _loadSystems.Cleanup();
    }
  }
}