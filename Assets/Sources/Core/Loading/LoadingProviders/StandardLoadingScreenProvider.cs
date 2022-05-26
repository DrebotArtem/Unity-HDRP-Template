using DrebotGS.Config;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DrebotGS.Core.Loading
{
  public sealed class StandardLoadingScreenProvider : LoadingScreenProvider
  {
    private GameAssetsCatalogue _gameAssetsCatalogue;
    protected override AssetReference _sceneLoadingReference => _gameAssetsCatalogue.loadingScreen;

    [Inject]
    public void Inject(GameAssetsCatalogue gameAssetsCatalogue, DiContainer diContainer)
    {
      _gameAssetsCatalogue = gameAssetsCatalogue;
      _diContainer = diContainer;
    }
  }
}