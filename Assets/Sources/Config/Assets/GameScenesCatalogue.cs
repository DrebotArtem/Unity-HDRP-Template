using UnityEngine;
using UnityEngine.AddressableAssets;


namespace DrebotGS.Config
{
  /// <summary>
  /// References to addressable scenes
  /// </summary>
  [CreateAssetMenu(fileName = "New GameScenesCatalogue", menuName = "DrebotGS/Assets/GameScenesCatalogue")]
  public class GameScenesCatalogue : ScriptableObject
  {
    public AssetReference IntroScene;
    public AssetReference LoadingScene;
    public AssetReference TitileScene;
  }
}