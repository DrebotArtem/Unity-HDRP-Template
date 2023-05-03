using System.Threading.Tasks;

namespace DrebotGS.Core.Loading
{
  public interface ILoadingProvider
  {
    Task LoadProvider();
    Task LoadOperations();
    Task UnloadProvider();
    void UnloadOperations();
  }
}