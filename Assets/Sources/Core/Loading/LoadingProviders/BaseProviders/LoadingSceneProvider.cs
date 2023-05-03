using System.Collections.Generic;
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
    private Queue<ILoadingOperation> _loadingOperations;

    public LoadingSceneProvider(AssetReference sceneLoadingReference)
    {
      _sceneLoadingReference = sceneLoadingReference;
    }

    public void SetLoadingOperation(Queue<ILoadingOperation> loadingOperations)
    {
      _loadingOperations = loadingOperations;
    }

    public async Task LoadProvider()
    {
      await LoadInternal(_sceneLoadingReference);
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _loadingOperations)
        await operation.Load(null);
    }

    public async Task UnloadProvider()
    {
      await ActivateLoadingOperations();
      UnloadInternal();
    }

    private async Task ActivateLoadingOperations()
    {
      foreach (var operation in _loadingOperations)
        await operation.Activate();
    }
    public void UnloadOperations()
    {
      foreach (var operation in _loadingOperations)
        operation.Unload();
    }
  }
}