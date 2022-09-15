using Entitas;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DrebotGS.Services
{
  public interface ILoadService
  {
    Task<T> LoadAsset<T>(IEntity entity, string assetRference);
  }
}