using DrebotGS.Core.Loading;
using DrebotGS.Systems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DrebotGS
{
  public class AppScene : MonoBehaviour
  {
    [Inject] private DiContainer _container;
    [Inject] private Contexts _contexts;

    private InjectableFeature _loadSystems;

    [Inject] LoadIntroSceneOperation _loadIntroSceneOperation;
    [Inject] LoadingSceneProvider loadingSceneProvider;
    [Inject] LoadingScreenProvider _loadingScreenProvider;

    void Start()
    {
      CreateAndInitLoadSystems();

      // Just example load Intro scene throw loading screen
      LoadIntroScene();
    }

    private void CreateAndInitLoadSystems()
    {
      _loadSystems = new InjectableFeature("LoadSystems");
      CreateLoadSystems(_contexts);
      _loadSystems.IncjectSelfAndChildren(_container);
      _loadSystems.Initialize();

      void CreateLoadSystems(Contexts contexts)
      {
        _loadSystems.Add(new LoadSceneSystem(contexts));
        _loadSystems.Add(new ProcessLoadingOperationSystem(contexts));
        _loadSystems.Add(new UnloadProviderSystem(contexts));

        _loadSystems.Add(new DestroyDestroyedGameStateSystem(contexts));
      }
    }

    private void LoadIntroScene()
    {
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(_loadIntroSceneOperation);
      _contexts.gameState.CreateLoadingProvider(_loadingScreenProvider, loadingOperations);
    }

    private void Update()
    {
      _loadSystems.Execute();
      _loadSystems.Cleanup();
    }
  }
}