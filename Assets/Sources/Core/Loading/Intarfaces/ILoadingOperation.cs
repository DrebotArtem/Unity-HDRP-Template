using System;
using System.Threading.Tasks;

namespace DrebotGS.Core.Loading
{
  public interface ILoadingOperation
  {
    Task Load(Action<float> onProgress);
    void Activate();
    void Unload();
  }
}