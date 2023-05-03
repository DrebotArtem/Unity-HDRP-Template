using DrebotGS.Config;
using DrebotGS.Core.Loading;
using DrebotGS.Factories;
using DrebotGS.Services;
using System.Collections.Generic;
using Zenject.Asteroids;

namespace DrebotGS.StateMachine
{
  public class MainMenuState : State
  {
    private readonly GameScenesCatalogue _gameScenesCatalogue;
    private readonly LoadingProvidersFactory _loadingProvidersFactory;
    private readonly SceneLoaderService _sceneLoaderService;
    //private readonly InputMasterService _inputMasterService;
    private readonly InputMasterControls _inputMasterControl;

    public MainMenuState(GameScenesCatalogue gameScenesCatalogue,
      LoadingProvidersFactory loadingProvidersFactory,
      SceneLoaderService sceneLoaderService)//,
     // InputMasterService inputMasterService)
    {
      _gameScenesCatalogue = gameScenesCatalogue;
      _loadingProvidersFactory = loadingProvidersFactory;
      _sceneLoaderService = sceneLoaderService;
      //_inputMasterService = inputMasterService;
      //_inputMasterControl = _inputMasterService.InputMasterControls;
    }

    public override void Start()
    {
      LoadMainMenuScene();
      //_inputMasterService.ToggleActionMap(_inputMasterControl.MainMenuControls);
    }

    public void LoadMainMenuScene()
    {
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.MainMenuScene));
      var emptyProvider = _loadingProvidersFactory.CreateEmptyProvider(loadingOperations);
      _sceneLoaderService.LoadProvider(emptyProvider);
    }
  }
}