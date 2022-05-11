using UnityEngine;

namespace DrebotGS.Core.Loading
{
  public class LoadingScreen : MonoBehaviour
  {
    public void OnProgress(float progress)
    {
      Debug.LogWarning("Progress loading: " + progress);
    }
  }
}