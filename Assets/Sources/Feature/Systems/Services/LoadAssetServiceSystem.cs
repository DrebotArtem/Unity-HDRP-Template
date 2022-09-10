using DrebotGS.Services;
using Entitas;
using Zenject;

namespace DrebotGS.Systems
{
  public class LoadAssetServiceSystem : IInitializeSystem
  {
    private readonly MetaContext _metaContext;
    private ILoadService _loadService;

    [Inject]
    public void Inject(ILoadService loadService)
    {
      _loadService = loadService;
    }

    public LoadAssetServiceSystem(Contexts contexts)
    {
      _metaContext = contexts.meta;
    }

    public void Initialize()
    {
      _metaContext.ReplaceLoadService(_loadService);
    }
  }
}