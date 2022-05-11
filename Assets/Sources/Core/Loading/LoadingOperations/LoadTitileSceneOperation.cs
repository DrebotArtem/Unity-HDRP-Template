using DrebotGS.Config;
using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace DrebotGS.Core.Loading
{
  public class LoadTitileSceneOperation : ILoadingOperation
  {
    private AsyncOperationHandle<SceneInstance> _loadOp;
    private GameScenesCatalogue _gameScenesCatalogue;

    [Inject]
    public void Inject(
      GameScenesCatalogue gameScenesCatalogue)
    {
      _gameScenesCatalogue = gameScenesCatalogue;
    }

    public async Task Load(Action<float> onProgress)
    {
      onProgress?.Invoke(0.5f);
      _loadOp = Addressables.LoadSceneAsync(_gameScenesCatalogue.TitileScene, LoadSceneMode.Additive, false);

      while (_loadOp.IsDone == false)
      {
        onProgress?.Invoke(_loadOp.PercentComplete);
        await Task.Delay(1);
      }
      onProgress?.Invoke(1f);
    }

    public void Activate()
    {
      _loadOp.Result.ActivateAsync();
    }

    public void Unload()
    {
      Addressables.UnloadSceneAsync(_loadOp);
    }
  }
}