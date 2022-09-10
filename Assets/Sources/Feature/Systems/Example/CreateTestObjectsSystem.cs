using Entitas;
using UnityEngine;

namespace DrebotGS.Systems
{
  public class CreateTestObjectsSystem : IInitializeSystem
  {
    private readonly Contexts _contexts;

    public CreateTestObjectsSystem(Contexts contexts)
    {
      _contexts = contexts;
    }

    public void Initialize()
    {
      Debug.Log("DestroyGameEntities");
      _contexts.game.DestroyAllEntities();
      _contexts.game.ResetCreationIndex();
      _contexts.game.Reset();

      CreateTestObject();
    }

    private void CreateTestObject()
    {
      GameEntity entity = _contexts.game.CreateEntity();
      entity.AddNameID("ExampleObject");
      entity.isAsset = true;
    }
  }
}

