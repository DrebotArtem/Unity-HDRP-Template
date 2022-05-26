using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// Load the scene by AssetReference.
  /// </summary>
  public abstract class LocalSceneLoader
  {
    private AsyncOperationHandle<SceneInstance> _cachedObject;

    protected async Task LoadInternal(string assetId)
    {
      _cachedObject = Addressables.LoadSceneAsync(assetId, UnityEngine.SceneManagement.LoadSceneMode.Additive);
      await _cachedObject.Task;
    }

    protected async Task LoadInternal(AssetReference assetReference)
    {
      _cachedObject = Addressables.LoadSceneAsync(assetReference, UnityEngine.SceneManagement.LoadSceneMode.Additive);
      await _cachedObject.Task;
    }

    protected void UnloadInternal()
    {
      if (!_cachedObject.IsDone)
        return;
      Addressables.UnloadSceneAsync(_cachedObject);
    }
  }
}