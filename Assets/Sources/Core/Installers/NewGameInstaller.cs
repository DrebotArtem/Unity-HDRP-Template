using DrebotGS.Systems;
using Zenject;

namespace DrebotGS.Core
{
  public class NewGameInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
    }
    private InjectableFeature _gameSystems;

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
    }

    private void OnEnable()
    {
      CreateGameSystems();
    }

    private void CreateGameSystems()
    {
      _gameSystems = new InjectableFeature("GameSystems");

      CreateLoadSystems(_contexts);
      _gameSystems.IncjectSelfAndChildren(_diContainer);
      _gameSystems.Initialize();

      void CreateLoadSystems(Contexts contexts)
      {
        _gameSystems.Add(new CreateTestObjectsSystem(contexts));

        _gameSystems.Add(new LoadAssetSystem(contexts));

        // Generated
        _gameSystems.Add(new GameEventSystems(contexts));
      }
    }

    private void Update()
    {
      _gameSystems.Execute();
      _gameSystems.Cleanup();
    }
  }
}