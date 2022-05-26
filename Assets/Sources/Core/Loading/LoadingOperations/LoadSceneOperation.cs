using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace DrebotGS.Core.Loading
{
  public class LoadSceneOperation : ILoadingOperation
  {
    private AsyncOperationHandle<SceneInstance> _loadOperation;
    private AssetReference _sceneReference;

    public LoadSceneOperation(AssetReference sceneReference)
    {
      _sceneReference = sceneReference;
    }

    public async Task Load(Action<float> onProgress)
    {
      onProgress?.Invoke(0.5f);
      _loadOperation = Addressables.LoadSceneAsync(_sceneReference, LoadSceneMode.Additive, false);

      while (_loadOperation.IsDone == false)
      {
        onProgress?.Invoke(_loadOperation.PercentComplete);
        await Task.Delay(1);
      }
      onProgress?.Invoke(1f);
    }

    public async Task Activate()
    {
      var act = _loadOperation.Result.ActivateAsync();
      while (!act.isDone)
      {
        await Task.Delay(1);
      }
    }

    public void Unload()
    {
      Addressables.UnloadSceneAsync(_loadOperation);
    }
  }
}