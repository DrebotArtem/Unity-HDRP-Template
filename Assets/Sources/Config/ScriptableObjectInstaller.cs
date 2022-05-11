using DrebotGS.Config;
using UnityEngine;
using Zenject;

namespace DrebotGS.Config
{
  /// <summary>
  /// Installer with SO dependencies (mostly Game Design configs)
  /// </summary>
  [CreateAssetMenu(fileName = "New ScriptableObjectInstaller", menuName = "DrebotGS/Configs/SOInstaller")]
  public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
  {
    [Header("Assets configuration")]
    public GameScenesCatalogue GameScenesCatalogue;
    public GameAssetsCatalogue AssetsCatalogue;

    [Header("Game data configuration")]
    public GameConfig GameConfig;
    public PlayerConfig PlayerConfig;

    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<GameScenesCatalogue>().FromInstance(GameScenesCatalogue).AsSingle();
      Container.BindInterfacesAndSelfTo<GameAssetsCatalogue>().FromInstance(AssetsCatalogue).AsSingle();
      Container.BindInterfacesAndSelfTo<GameConfig>().FromInstance(GameConfig).AsSingle();
      Container.BindInterfacesAndSelfTo<PlayerConfig>().FromInstance(PlayerConfig).AsSingle();
    }
  }
}