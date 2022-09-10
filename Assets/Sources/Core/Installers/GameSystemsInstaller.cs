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
    private InjectableFeature _serviceRegisterSystems;
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
      CreateAndInitRegisterServiceSystems();
      CreateAndInitLoadSystems();

      var name = SceneManager.GetActiveScene().name;
      AddSupportScene();
      LoadFirstScene(name);
    }

    private void CreateAndInitRegisterServiceSystems()
    {
      _serviceRegisterSystems = new InjectableFeature("ServiceSystems");
      CreateRegisterServiceSystems(_contexts);
      _serviceRegisterSystems.IncjectSelfAndChildren(_diContainer);
      _serviceRegisterSystems.Initialize();

#if UNITY_EDITOR
      DontDestroyOnLoad(_serviceRegisterSystems.gameObject);
#endif

      void CreateRegisterServiceSystems(Contexts contexts)
      {
        _serviceRegisterSystems.Add(new LoadAssetServiceSystem(contexts));
      }
    }
    private void CreateAndInitLoadSystems()
    {
      _loadSystems = new InjectableFeature("LoadSystems");
      CreateLoadSystems(_contexts);
      _loadSystems.IncjectSelfAndChildren(_diContainer);
      _loadSystems.Initialize();

#if UNITY_EDITOR
      DontDestroyOnLoad(_loadSystems.gameObject);
#endif

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
      _serviceRegisterSystems.Execute();
      _loadSystems.Execute();

      _serviceRegisterSystems.Cleanup();
      _loadSystems.Cleanup();
    }

    private void AddSupportScene()
    {
      SceneManager.LoadScene(_supportSceneName);
    }

    /// <summary>
    /// Only for debug. Needed to correctly launch the game from any scene.
    /// </summary>
    /// <param name="name"></param>
    private void LoadFirstScene(string name)
    {
      _loadingSceneHelper.LoadFirstSceneByName(name);
    }
  }
}