using DrebotGS.Services;
using Entitas;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace DrebotGS.Systems
{
  public class LoadAssetSystem : ReactiveSystem<GameEntity>
  {
    readonly Contexts _contexts;
    ILoadService _viewService;

    [Inject]
    public void Inject(ILoadService viewService)
    {
      _viewService = viewService;
    }
    public LoadAssetSystem(Contexts contexts) : base(contexts.game)
    {
      _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.Asset);

    protected override bool Filter(GameEntity entity)
        => entity.isAsset && entity.hasNameID && !entity.hasView; //&& entity.hasTramsform3D;

    protected override void Execute(List<GameEntity> entities)
    {
      foreach (var entity in entities)
      {
        InstantiateAsset(entity);
      }
    }

    private async void InstantiateAsset(GameEntity entity)
    {
      var viewGO = await _viewService.LoadAsset<Transform>(entity, entity.nameID.value);
      if (entity.isEnabled == false || viewGO == null)
        return;

      //SetParent(viewGO, entity);
      //SetTransform(viewGO, entity);
      var view = viewGO.GetComponent<IView>();
      view.Link(entity);
      entity.AddView(view);
    }

    //private void SetTransform(GameObject viewGameObject, GameEntity entity)
    //{
    //  viewGameObject.transform.localPosition = new Vector3(
    //    entity.tramsform3D.value.Position[0],
    //    entity.tramsform3D.value.Position[1],
    //    entity.tramsform3D.value.Position[2]);

    //  viewGameObject.transform.localRotation = Quaternion.Euler(
    //    entity.tramsform3D.value.Rotation[0],
    //    entity.tramsform3D.value.Rotation[1],
    //    entity.tramsform3D.value.Rotation[2]);

    //  viewGameObject.transform.localScale = new Vector3(
    //    entity.tramsform3D.value.Scale[0],
    //    entity.tramsform3D.value.Scale[1],
    //    entity.tramsform3D.value.Scale[2]);
    //}

    //private void SetParent(GameObject viewGameObject, GameEntity entity)
    //{
    //  viewGameObject.transform.SetParent(_locationView.GetTransformArea(entity.area.areaName));
    //}
  }
}