using UnityEngine;
using UnityEngine.UIElements;

namespace DrebotGS.Core.UI
{
  [RequireComponent(typeof(UIDocument))]
  public class LoadingScreenUI : MonoBehaviour
  {
    private VisualElement loadingPanel;

    public void Awake()
    {
      loadingPanel = GetComponent<UIDocument>().rootVisualElement;

      //DisableGameScreen();
    }

    public void EnableGameScreen()
    {
      loadingPanel.style.display = DisplayStyle.Flex;
      loadingPanel.SetEnabled(true);
    }

    public void DisableGameScreen()
    {
      loadingPanel.style.display = DisplayStyle.None;
      loadingPanel.SetEnabled(false);
    }
  }
}