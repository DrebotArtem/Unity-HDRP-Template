using Entitas;
using System.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace DrebotGS.Services
{
  public class UnityResourcesLoadService : ILoadService
  {
    private DiContainer _container;

    public UnityResourcesLoadService(DiContainer container)
    {
      _container = container;
    }
    public async Task<T> LoadAsset<T>(IEntity entity, string assetRference)
    {
      var viewGo = _container.InstantiatePrefab(Resources.Load<GameObject>(assetRference));
      if (viewGo.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }
  }
}
