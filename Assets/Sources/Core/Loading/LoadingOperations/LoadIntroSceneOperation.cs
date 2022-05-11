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
  public class LoadIntroSceneOperation : ILoadingOperation
  {
    private AsyncOperationHandle<SceneInstance> _loadOperation;
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
      _loadOperation = Addressables.LoadSceneAsync(_gameScenesCatalogue.IntroScene, LoadSceneMode.Additive, false);

      while (_loadOperation.IsDone == false)
      {
        onProgress?.Invoke(_loadOperation.PercentComplete);
        await Task.Delay(1);
      }
      onProgress?.Invoke(1f);
    }

    public void Activate()
    {
      _loadOperation.Result.ActivateAsync();
    }

    public void Unload()
    {
      Addressables.UnloadSceneAsync(_loadOperation);
    }
  }
}