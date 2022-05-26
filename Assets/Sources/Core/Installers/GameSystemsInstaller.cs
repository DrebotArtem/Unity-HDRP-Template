using DrebotGS.Systems;
using UnityEngine.SceneManagement;
using Zenject;

namespace DrebotGS.Core
{
  public class GameSystemsInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
    }

    private InjectableFeature _loadSystems;
    private const string _supportSceneName = "SupportScene";

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

    public override void Start()
    {
      CreateAndInitLoadSystems();

      var name = SceneManager.GetActiveScene().name;
      AddSupportScene();
      LoadFirstScene(name);
    }

    private void CreateAndInitLoadSystems()
    {
      _loadSystems = new InjectableFeature("LoadSystems");
      CreateLoadSystems(_contexts);
      _loadSystems.IncjectSelfAndChildren(_diContainer);
      _loadSystems.Initialize();
      DontDestroyOnLoad(_loadSystems.gameObject);
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

    private void AddSupportScene()
    {
      SceneManager.LoadScene(_supportSceneName);
    }

    /// <summary>
    /// Inly for debug. Needed to correctly launch the game from any scene.
    /// </summary>
    /// <param name="name"></param>
    private void LoadFirstScene(string name)
    {
      _loadingSceneHelper.LoadFirstSceneByName(name);
    }
  }
}