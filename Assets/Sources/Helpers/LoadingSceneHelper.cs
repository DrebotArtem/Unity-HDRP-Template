using DrebotGS.Config;
using DrebotGS.Core.Loading;
using System.Collections.Generic;
using Zenject;
using UnityEngine.AddressableAssets;

namespace DrebotGS
{
  public class LoadingSceneHelper
  {
    //Inject
    private Contexts _contexts;
    private GameScenesCatalogue _gameScenesCatalogue;
    private StandardLoadingScreenProvider _standardLoadingScreenProvider;

    [Inject]
    public void Inject(Contexts contexts,
      GameScenesCatalogue gameScenesCatalogue,
      StandardLoadingScreenProvider standardLoadingScreenProvider)
    {
      _contexts = contexts;
      _gameScenesCatalogue = gameScenesCatalogue;
      _standardLoadingScreenProvider = standardLoadingScreenProvider;
    }

    /// <summary>
    ///  Only for debug. This method is needed to correctly launch the game from any scene.
    ///  Here you need to add a call to all the scenes from which you will launch the game in debug mode.
    /// </summary>
    /// <param name="nameFirstScene"></param>
    public void LoadFirstSceneByName(string nameFirstScene)
    {
      if (nameFirstScene == "AppScene")
        return;

      var fieldScene = typeof(GameScenesCatalogue).GetField(nameFirstScene);
      if (fieldScene == null)
        throw new System.NullReferenceException($"The GameScenesCatalogue class does't contain field {nameFirstScene}. " +
                                                "Please check the name of the scene you are loading.");

      AssetReference firstScene = fieldScene.GetValue(_gameScenesCatalogue) as AssetReference;
      CreateLoadingProviderForFirstScene();

      void CreateLoadingProviderForFirstScene()
      {
        var loadingSceneProvider = new LoadingEmptyProvider();
        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new LoadSceneOperation(firstScene));
        _contexts.gameState.CreateLoadingProvider(loadingSceneProvider, loadingOperations);
      }
    }

    public void LoadIntroScene()
    {
      var loadingSceneProvider = new LoadingEmptyProvider();
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.IntroScene));
      _contexts.gameState.CreateLoadingProvider(loadingSceneProvider, loadingOperations);
    }

    public void LoadMainMenuScene()
    {
      var loadingSceneProvider = new LoadingSceneProvider(_gameScenesCatalogue.BaseLoadingScene);
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.MainMenuScene));
      _contexts.gameState.CreateLoadingProvider(loadingSceneProvider, loadingOperations, false);
    }

    public void LoadNewGameScene()
    {
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.NewGameScene));
      _contexts.gameState.CreateLoadingProvider(_standardLoadingScreenProvider, loadingOperations, false);
    }
  }
}