using DrebotGS.Core.Loading;
using System.Collections.Generic;


public static class LoadContextExtension
{
  public static GameStateEntity CreateLoadingProvider(this GameStateContext context,
    ILoadingProvider loadingScreenProvider,
     Queue<ILoadingOperation> loadingOperations,
     bool unloadProviderAfterLoading = true)
  {
    var ent = context.CreateEntity();
    ent.AddLoadingProvider(loadingScreenProvider, loadingOperations);
    ent.isUnloadProviderAfterLoad = unloadProviderAfterLoading;
    return ent;
  }
}
