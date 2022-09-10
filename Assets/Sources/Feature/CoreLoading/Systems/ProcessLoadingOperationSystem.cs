using System.Collections.Generic;
using Entitas;

public class ProcessLoadingOperationSystem : ReactiveSystem<GameStateEntity>
{
  public ProcessLoadingOperationSystem(Contexts contexts) : base(contexts.gameState) { }

  protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context)
    => context.CreateCollector(GameStateMatcher.LoadedProvider);

  protected override bool Filter(GameStateEntity entity)
    => entity.isLoadedProvider;


  protected override void Execute(List<GameStateEntity> entities)
  {
    foreach (var entity in entities)
    {
      ReplaceStatusProvider(entity, StatusProvider.LoadingOperations);
      LoadOperators(entity);
    }
  }

  private void ReplaceStatusProvider(GameStateEntity entity, StatusProvider status)
  {
    entity.ReplaceStatusProvider(status);
  }

  private void LoadOperators(GameStateEntity entity)
  {
    entity.loadingProvider.provider.LoadOperations();
  }
}