using System.Collections.Generic;
using Entitas;

public class UnloadProviderSystem : ReactiveSystem<GameStateEntity>
{
  public UnloadProviderSystem(Contexts contexts) : base(contexts.gameState) { }

  protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context)
  {
    return context.CreateCollector(GameStateMatcher.UnloadProvider);
  }

  protected override bool Filter(GameStateEntity entity)
  {
    return entity.hasLoadingProvider && entity.isUnloadProvider;
  }

  protected override void Execute(List<GameStateEntity> entities)
  {
    foreach (var e in entities)
    {
      e.loadingProvider.provider.UnloadProvider();
      e.ReplaceStatusProvider(StatusProvider.Loaded);
    }
  }
}