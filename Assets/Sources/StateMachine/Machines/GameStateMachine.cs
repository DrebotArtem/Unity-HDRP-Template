using System.Collections.Generic;
using System.Net.NetworkInformation;
using Zenject;
using Zenject.Asteroids;

namespace DrebotGS.StateMachine
{
  public class GameStateMachine : StateMachine
  {
    private readonly DiContainer _container;

    public GameStateMachine(DiContainer container)
    {
      _container = container;
      Initialize();
    }

    public void Initialize()
    {
      var introState = _container.Instantiate<IntroState>();
      var mainMenuState = _container.Instantiate<MainMenuState>();
      var newGameState = _container.Instantiate<NewGameState>();

      AddState<IntroState>(introState);
      AddState<MainMenuState>(mainMenuState);
      AddState<NewGameState>(newGameState);
    }
  }
}