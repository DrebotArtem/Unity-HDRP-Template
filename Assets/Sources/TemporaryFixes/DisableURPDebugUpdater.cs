using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// This script temporarily fixes the issue until Unity fixes it.
/// https://issuetracker.unity3d.com/issues/isdebugbuild-returns-false-in-the-editor-when-its-value-is-checked-after-a-build
/// https://forum.unity.com/threads/errors-with-the-urp-debug-manager.987795/#post-8055599
/// </summary>

public class DisableURPDebugUpdater : MonoBehaviour
{
  private void Awake()
  {
    //DebugManager.instance.enableRuntimeUI = false;
  }
}