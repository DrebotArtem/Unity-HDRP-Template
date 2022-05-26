using System.Threading.Tasks;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// LoadingEmptyProvider is used to load the scene, without intermediate objects.
  /// </summary>
  public class LoadingEmptyProvider : ILoadingProvider
  {
    private GameStateEntity _entityLoadingProvider;

    public async Task LoadProvider(GameStateEntity entityLoadingPrivider)
    {
      _entityLoadingProvider = entityLoadingPrivider;
      await Task.Delay(1);
      _entityLoadingProvider.isLoadedProvider = true;
    }

    public async Task LoadOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
        await operation.Load(null);

      await Task.Delay(1);
      _entityLoadingProvider.isLoadedOperations = true;
      await Task.Delay(1);
      _entityLoadingProvider.isUnloadProvider = _entityLoadingProvider.isUnloadProviderAfterLoad;
    }

    public async void UnloadProvider()
    {
      await ActivateLoadingOperations();
    }

    private async Task ActivateLoadingOperations()
    {
      foreach (var operation in _entityLoadingProvider.loadingProvider.loadingOperations)
        await operation.Activate();
    }
  }
}