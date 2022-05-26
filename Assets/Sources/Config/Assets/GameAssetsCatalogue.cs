using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DrebotGS.Config
{
  /// <summary>
  /// References to addressable assets
  /// </summary>
  [CreateAssetMenu(fileName = "New GameAssetsCatalogue", menuName = "DrebotGS/Assets/GameAssetsCatalogue")]
  public class GameAssetsCatalogue : ScriptableObject
  {
    [Header("Loading Screens")]
    public AssetReference loadingScreen;
  }
}
