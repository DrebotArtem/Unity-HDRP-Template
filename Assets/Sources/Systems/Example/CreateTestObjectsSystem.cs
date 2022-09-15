using Entitas;

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
      CreateTestObject();
    }

    private void CreateTestObject()
    {
      GameEntity entity = _contexts.game.CreateEntity();
      entity.AddNameID("ExampleObject");
      entity.isAsset = true;
      GameEntity entity2 = _contexts.game.CreateEntity();
      entity2.AddNameID("ExampleObject");
      entity2.isAsset = true;
      GameEntity entity3 = _contexts.game.CreateEntity();
      entity3.AddNameID("ExampleObject");
      entity3.isAsset = true;
    }
  }
}

