using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// WARNING!!! Injection for prefab occurs after the Start. 
  /// After the prefab is loaded, the InjectGameObject is used.
  /// LoadingScreenProvider is used as an prefab while we wait for the next scene to load.
  /// </summary>
  public abstract class LoadingScreenProvider : LocalAssetLoader, ILoadingProvider
  {
    private GameStateEntity _entityLoadingProvider;
    private LoadingScreen _loadingScreen;

    protected abstract AssetReference _sceneLoadingReference { get; }
    protected DiContainer _diContainer;

    public async Task LoadOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
      {
        await operation.Load(_loadingScreen.OnProgress);
      }
      await Task.Delay(1);
      _entityLoadingProvider.isLoadedOperations = true;
      await Task.Delay(1);
      _entityLoadingProvider.isUnloadProvider = _entityLoadingProvider.isUnloadProviderAfterLoad;
    }

    public async Task LoadProvider(GameStateEntity entity)
    {
      _entityLoadingProvider = entity;
      _loadingScreen = await Load();
      _diContainer.InjectGameObject(_loadingScreen.gameObject);
      _entityLoadingProvider.isLoadedProvider = true;
    }

    private Task<LoadingScreen> Load()
    {
      return LoadInternal<LoadingScreen>(_sceneLoadingReference);
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