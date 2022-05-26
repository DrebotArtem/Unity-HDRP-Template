using System;
using System.Threading.Tasks;

namespace DrebotGS.Core.Loading
{
  public interface ILoadingOperation
  {
    Task Load(Action<float> onProgress);
    Task Activate();
    void Unload();
  }
}