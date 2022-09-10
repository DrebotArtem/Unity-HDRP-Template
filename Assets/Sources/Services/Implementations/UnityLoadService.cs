using Entitas;
using UnityEngine;
using Zenject;

namespace DrebotGS.Services
{
  public class UnityLoadService : ILoadService
  {
    private DiContainer _container;

    public UnityLoadService(DiContainer container)
    {
      _container = container;
    }

    public GameObject LoadAsset(Contexts contexts, IEntity entity, string assetName)
    {
      var viewGo = _container.InstantiatePrefab(Resources.Load<GameObject>(assetName)); 
      return viewGo;
    }
  }
}
