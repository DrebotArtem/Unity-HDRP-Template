using Entitas;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

[Game]
public sealed class LoadAsyncComponent : IComponent
{
  public AsyncOperationHandle<GameObject> value;
}
