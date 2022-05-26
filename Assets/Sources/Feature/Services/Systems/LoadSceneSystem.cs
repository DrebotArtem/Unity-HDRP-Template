using System.Collections.Generic;
using Entitas;

public class LoadSceneSystem : ReactiveSystem<GameStateEntity>
{
  private readonly IGroup<GameStateEntity> _providers;
  public LoadSceneSystem(Contexts contexts) : base(contexts.gameState)
  {
    _providers = contexts.gameState.GetGroup(GameStateMatcher.AllOf(GameStateMatcher.StatusProvider));
  }

  protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context)
  {
    return context.CreateCollector(GameStateMatcher.LoadingProvider);
  }

  protected override bool Filter(GameStateEntity entity)
  {
    return entity.hasLoadingProvider;
  }

  protected override void Execute(List<GameStateEntity> entities)
  {
    foreach (var provider in _providers)
    {
      if (provider.statusProvider.status == StatusProvider.Loaded)
      {
        foreach (var operation in provider.loadingProvider.loadingOperations)
        {
          operation.Unload();
        }

        provider.isDestroyed = true;
      }
    }

    foreach (var e in entities)
    {
      e.AddStatusProvider(StatusProvider.LoadingPrivider);
      e.loadingProvider.provider.LoadProvider(e);
    }
  }
}