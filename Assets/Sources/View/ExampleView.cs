using Entitas;
using Entitas.Unity;
using UnityEditor.SceneManagement;
namespace DrebotGS.Views
{
  public class ExampleView : BaseView, IGameDestroyedListener
  {
    public override void Link(IEntity entity)
    {
      base.Link(entity);
      AddListeners();
    }

    public void OnDestroyed(GameEntity entity)
    {
      RemoveComponents();
      RemoveListeners();
      Unlink();
      Destroy(gameObject);
    }

    private void AddListeners()
    {
      Entity.AddGameDestroyedListener(this);
    }

    private void RemoveListeners()
    {
      Entity.RemoveGameDestroyedListener(this);
    }

    private void RemoveComponents()
    {       
      Entity.RemoveView();
      Entity.isAsset = false;
    }

    private void OnDestroy()
    {
      if (Entity == null) return;
      RemoveComponents();
      RemoveListeners();
      Unlink();
    }
  }
}