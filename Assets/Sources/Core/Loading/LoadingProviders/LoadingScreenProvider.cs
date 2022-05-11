using DrebotGS.Config;
using System.Threading.Tasks;
using Zenject;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// LoadingScene is used as an intermediate scene while we wait for the main scene to load.
  /// </summary>
  public class LoadingScreenProvider : LocalAssetLoader, ILoadingProvider
  {
    private GameStateEntity _entityLoadingProvider;
    private LoadingScreen _loadingScreen;

    private GameAssetsCatalogue _gameAssetsCatalogue;

    [Inject]
    public void Inject(
      GameAssetsCatalogue gameAssetsCatalogue)
    {
      _gameAssetsCatalogue = gameAssetsCatalogue;
    }
    public void SetEntity(GameStateEntity entity)
    {
      _entityLoadingProvider = entity;
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
      {
        await operation.Load(_loadingScreen.OnProgress);
      }
      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isLoadedOperations = true;
      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isUnloadProvider = _entityLoadingProvider.isUnloadProviderAfterLoad;
    }

    public async Task LoadProvider(GameStateEntity entity)
    {
      _entityLoadingProvider = entity;
      _loadingScreen = await Load();
      _entityLoadingProvider.isLoadedProvider = true;
    }

    private Task<LoadingScreen> Load()
    {
      return LoadInternal<LoadingScreen>(_gameAssetsCatalogue.loadingScreen);
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