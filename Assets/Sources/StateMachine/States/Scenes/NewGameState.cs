using DrebotGS.Config;
using DrebotGS.Core.Loading;
using DrebotGS.Factories;
using DrebotGS.Services;
using System.Collections.Generic;
using Zenject.Asteroids;

namespace DrebotGS.StateMachine
{
  public class NewGameState : State
  {
    private readonly GameScenesCatalogue _gameScenesCatalogue;
    private readonly LoadingProvidersFactory _loadingProvidersFactory;
    private readonly SceneLoaderService _sceneLoaderService;

    public NewGameState(GameScenesCatalogue gameScenesCatalogue,
      LoadingProvidersFactory loadingProvidersFactory,
      SceneLoaderService sceneLoaderService)
    {
      _gameScenesCatalogue = gameScenesCatalogue;
      _sceneLoaderService = sceneLoaderService;
      _loadingProvidersFactory = loadingProvidersFactory;
    }

    public override void Start()
    {
      LoadMainHouseScene();
    }

    public void LoadMainHouseScene()
    {
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.NewGameScene));
      var standardLoadingScreenProvider = _loadingProvidersFactory.CreateStandardScreenProvider(loadingOperations);
      _sceneLoaderService.LoadProvider(standardLoadingScreenProvider);
    }
  }
}