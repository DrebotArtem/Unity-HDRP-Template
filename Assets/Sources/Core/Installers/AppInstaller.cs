using DrebotGS;
using DrebotGS.StateMachine;
using Zenject;

public class AppInstaller : MonoInstaller
{
  private GameStateMachine _gameStateMachine;

  public override void InstallBindings()
  {
  }

  [Inject]
  public void Inject(GameStateMachine gameStateMachine)
  {
    _gameStateMachine = gameStateMachine;
  }

  public override void Start()
  {
    _gameStateMachine.SwitchState<IntroState>();
    // Just example load Intro scene
    //_loadingSceneHelper.LoadIntroScene();
  }
}