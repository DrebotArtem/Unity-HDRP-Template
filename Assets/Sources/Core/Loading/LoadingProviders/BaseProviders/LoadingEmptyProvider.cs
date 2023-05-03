using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// LoadingEmptyProvider is used to load the scene, without intermediate objects.
  /// </summary>
  public class LoadingEmptyProvider : ILoadingProvider
  {
    private Queue<ILoadingOperation> _loadingOperations;

    public LoadingEmptyProvider(
      Queue<ILoadingOperation> loadingOperations)
    {
      _loadingOperations = loadingOperations;
    }

    public async Task LoadProvider()
    {
      await Task.Delay(1);
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _loadingOperations)
        await operation.Load(null);
    }

    public async Task UnloadProvider()
    {
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