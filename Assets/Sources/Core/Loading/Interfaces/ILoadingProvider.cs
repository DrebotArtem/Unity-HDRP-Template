using System.Threading.Tasks;

namespace DrebotGS.Core.Loading
{
  public interface ILoadingProvider
  {
    Task LoadProvider(GameStateEntity entity);
    Task LoadOperations();
    void UnloadProvider();
  }
}