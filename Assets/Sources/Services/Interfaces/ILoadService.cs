using Entitas;
using UnityEngine;

namespace DrebotGS.Services
{
  public interface ILoadService
  {
    GameObject LoadAsset(
    Contexts contexts,
    IEntity entity,
    string assetName);
  }
}