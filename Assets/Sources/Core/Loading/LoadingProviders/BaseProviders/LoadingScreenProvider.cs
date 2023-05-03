using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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
    private LoadingScreen _loadingScreen;

    protected abstract AssetReference _sceneLoadingReference { get; }
    protected DiContainer _diContainer;

    private Queue<ILoadingOperation> _loadingOperations;

    public void SetLoadingOperation(Queue<ILoadingOperation> loadingOperations)
    {
      _loadingOperations = loadingOperations;
    }

    public async Task LoadOperations()
    {
        foreach (var operation in _loadingOperations)
          await operation.Load(_loadingScreen.OnProgress);
    }

    public async Task LoadProvider()
    {
      _loadingScreen = await Load();
      _diContainer.InjectGameObject(_loadingScreen.gameObject);
    }

    private Task<LoadingScreen> Load()
    {
      return LoadInternal<LoadingScreen>(_sceneLoadingReference);
    }

    public async Task UnloadProvider()
    {
      UnloadInternal();
      await ActivateLoadingOperations();
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