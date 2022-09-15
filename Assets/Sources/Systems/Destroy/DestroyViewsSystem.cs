using Entitas;
using System.Collections.Generic;

namespace DrebotGS.Systems
{
  public class DestroyViewsSystem : ITearDownSystem
  {
    private readonly IGroup<GameEntity> _groupView;
    private readonly List<GameEntity> _bufferView = new List<GameEntity>();

    public DestroyViewsSystem(Contexts contexts)
    {
      _groupView = contexts.game.GetGroup(GameMatcher.View);
    }

    public void TearDown()
    {
      foreach (var item in _groupView.GetEntities(_bufferView))
      {
        item.isDestroyed = true;
      }
    }
  }
}