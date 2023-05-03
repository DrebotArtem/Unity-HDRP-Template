using DrebotGS.Core.Loading;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DrebotGS.Factories
{
  public class LoadingProvidersFactory
  {
    private readonly DiContainer _diContainer;

    public LoadingProvidersFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public ILoadingProvider CreateEmptyProvider(Queue<ILoadingOperation> loadingOperations)
    {
      LoadingEmptyProvider emptyProvider = new LoadingEmptyProvider(loadingOperations);
      return emptyProvider;
    }

    public ILoadingProvider CreateSceneProvider(AssetReference sceneLoadingReference, Queue<ILoadingOperation> loadingOperations)
    {
      LoadingSceneProvider sceneProvider = new LoadingSceneProvider(sceneLoadingReference);
      sceneProvider.SetLoadingOperation(loadingOperations);
      return sceneProvider;
    }

    public ILoadingProvider CreateStandardScreenProvider(Queue<ILoadingOperation> loadingOperations)
    {
      StandardLoadingScreenProvider screenProvider = _diContainer.Instantiate<StandardLoadingScreenProvider>();
      screenProvider.SetLoadingOperation(loadingOperations);
      return screenProvider;
    }
  }
}