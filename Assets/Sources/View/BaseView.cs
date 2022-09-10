using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace DrebotGS.Views
{
  public class BaseView : MonoBehaviour, IView
  {
    private GameEntity _entity;

    public GameEntity Entity
    {
      get => _entity;
    }

    public virtual void Link(IEntity entity)
    {
      gameObject.Link(entity);
      _entity = (GameEntity)entity;
    }

    public virtual void RemoveAsset()
    {
      Entity.RemoveView();
      Entity.isAsset = false;
    }
  }
}