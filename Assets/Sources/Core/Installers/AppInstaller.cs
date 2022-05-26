using DrebotGS;
using Zenject;

public class AppInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
  }

  //Injects
  private LoadingSceneHelper _loadingSceneHelper;

  [Inject]
  public void Inject(LoadingSceneHelper loadingSceneHelper)
  {
    _loadingSceneHelper = loadingSceneHelper;
  }

  public override void Start()
  {
    // Just example load Intro scene
    _loadingSceneHelper.LoadIntroScene();
  }
}