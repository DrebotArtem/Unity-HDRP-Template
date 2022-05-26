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
    [Header("Main Scenes")]
    public AssetReference IntroScene;
    public AssetReference MainMenuScene;

    [Header("Game Scenes")]
    public AssetReference NewGameScene;

    [Header("Loading Scenes")]
    public AssetReference BaseLoadingScene;
  }
}