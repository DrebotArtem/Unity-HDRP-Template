using Entitas;
using System.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using Zenject;

namespace DrebotGS.Services
{
  public class UnityAddressablesLoadService : ILoadService
  {
    private DiContainer _container;

    public UnityAddressablesLoadService(DiContainer container)
    {
      _container = container;
    }

    public async Task<T> LoadAsset<T>(IEntity entity, string assetRference)
    {
      var handle = Addressables.InstantiateAsync(assetRference);

      if (entity.GetType() == typeof(GameEntity))
      {
        var gameEntity = (GameEntity)entity;
        gameEntity.AddLoadAsync(handle);
      }

      var _cachedObject = await handle.Task;
      if (_cachedObject == null)
        return default(T);
      _container.InjectGameObject(_cachedObject);
      if (_cachedObject.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }
  }
}
