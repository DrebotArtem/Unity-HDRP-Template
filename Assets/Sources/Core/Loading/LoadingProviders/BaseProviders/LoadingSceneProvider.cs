using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// LoadingSceneProvider is used as an intermediate scene while we wait for the next scene to load.
  /// </summary>
  public class LoadingSceneProvider : LocalSceneLoader, ILoadingProvider
  {
    private GameStateEntity _entityLoadingProvider;
    private AssetReference _sceneLoadingReference;

    public LoadingSceneProvider(AssetReference sceneLoadingReference)
    {
      _sceneLoadingReference = sceneLoadingReference;
    }

    public async Task LoadProvider(GameStateEntity entityLoadingPrivider)
    {
      _entityLoadingProvider = entityLoadingPrivider;

      await LoadInternal(_sceneLoadingReference);
      _entityLoadingProvider.isLoadedProvider = true;
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
        await operation.Load(null);

      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isLoadedOperations = true;
      // We are waiting for some time to simulate the loading.
      await Task.Delay(2000);
      _entityLoadingProvider.isUnloadProvider = _entityLoadingProvider.isUnloadProviderAfterLoad;
    }

    public async void UnloadProvider()
    {
      await ActivateLoadingOperations();
      UnloadInternal();
    }

    private async Task ActivateLoadingOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
        await operation.Activate();
    }
  }
}