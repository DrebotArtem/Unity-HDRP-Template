using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DrebotGS.Core.Loading
{
  /// <summary>
  /// Load the asset by stringId from Addressables
  /// </summary>
  public abstract class LocalAssetLoader
  {
    private GameObject _cachedObject;

    protected async Task<T> LoadInternal<T>(string assetId)
    {
      var handle = Addressables.InstantiateAsync(assetId);
      _cachedObject = await handle.Task;
      if (_cachedObject.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }

    protected async Task<T> LoadInternal<T>(AssetReference assetRference)
    {
      var handle = Addressables.InstantiateAsync(assetRference);
      _cachedObject = await handle.Task;
      if (_cachedObject.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");

      return component;
    }

    protected void UnloadInternal()
    {
      if (_cachedObject == null)
        return;
      _cachedObject.SetActive(false);
      Addressables.ReleaseInstance(_cachedObject);
      _cachedObject = null;
    }
  }
}