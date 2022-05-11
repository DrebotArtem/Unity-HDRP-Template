using DrebotGS.Config;
using System.Threading.Tasks;
using Zenject;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// LoadingScene is used as an intermediate scene while we wait for the main scene to load.
  /// </summary>
  public class LoadingSceneProvider : LocalSceneLoader, ILoadingProvider
  {
    private GameStateEntity _entityLoadingProvider;
    private GameScenesCatalogue _gameScenesCatalogue;

    [Inject]
    public void Inject(
      GameScenesCatalogue gameScenesCatalogue)
    {
      _gameScenesCatalogue = gameScenesCatalogue;
    }

    public void SetEntity(GameStateEntity entity)
    {
      _entityLoadingProvider = entity;
    }

    public async Task LoadProvider(GameStateEntity entityLoadingPrivider)
    {
      _entityLoadingProvider = entityLoadingPrivider;

      await LoadInternal(_gameScenesCatalogue.LoadingScene);
      _entityLoadingProvider.isLoadedProvider = true;
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
      {
        await operation.Load(null);
      }
      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isLoadedOperations = true;
      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isUnloadProvider = _entityLoadingProvider.isUnloadProviderAfterLoad;
    }

    public void UnloadProvider()
    {
      UnloadInternal();
      ActivateLoadingOperations();
    }

    private void ActivateLoadingOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
      {
        operation.Activate();
      }
    }
  }
}