using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace DrebotGS.UI
{
  [RequireComponent(typeof(UIDocument))]
  public class MainMenuScreenMono : MonoBehaviour
  {
    private InputMasterControls _inputControl;

    private VisualElement _rootVisualElement;
    private VisualElement _titleScreenElement;
    private OptionsScreenVE _optionsScreenVE;
    private VisualElement _optionsScreenElement; // for binds

    private const string _titleScreenName = "TitleScreen";
    private const string _optionsScreenName = "OptionsScreen";

    private const string _titleGameLabelName = "title-game-label";
    private const string _titleButtonsPanel = "title-buttons-panel";
    private const string _titleNewGameButtonName = "new-game-button";
    private const string _titleOptionsButtonName = "options-button";
    private const string _titleExitButtonName = "exit-button";

    private const string _optionsReturnToTitleButtonName = "options-close-button";

    private const string _transitionOpacityOut = "opacity-out";
    private const string _transitionOpacityIn = "opacity-in";

    //Inject
    private LoadingSceneHelper _loadingSceneHelper;

    [Inject]
    public void Inject(LoadingSceneHelper loadingSceneHelper)
    {
      _loadingSceneHelper = loadingSceneHelper;
    }

    public void Awake()
    {
      InitInputControl();
      InitVisualElements();
      BindTitleButtons();
      BindButtonsReturnToMainMenu();
      BindOtherScreens();

      TransitionsAtStart();
    }

    private void InitInputControl()
    {
      _inputControl = new InputMasterControls();
      _inputControl.Enable();
    }

    private void InitVisualElements()
    {
      _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
      _titleScreenElement = _rootVisualElement.Q<VisualElement>(_titleScreenName);
      _optionsScreenVE = _rootVisualElement.Q<OptionsScreenVE>();
      _optionsScreenElement = _rootVisualElement.Q<VisualElement>(_optionsScreenName);
    }

    private void BindTitleButtons()
    {
      BindNewGameButton();
      BindOptionsButton();
      BindExitButton();

      void BindNewGameButton()
      {
        var newGame = _rootVisualElement.Q<Button>(_titleNewGameButtonName);
        if (newGame != null)
        {
          newGame.clickable.clicked += () =>
          {
            _loadingSceneHelper.LoadNewGameScene();
          };
        }
      }
      void BindOptionsButton()
      {
        var optionsGame = _rootVisualElement.Q<Button>(_titleOptionsButtonName);
        if (optionsGame != null)
        {
          optionsGame.clickable.clicked += () =>
          {
            ShowOptions();
            HideTitle();
          };
        }
        void ShowOptions()
        {
          _optionsScreenElement.style.display = DisplayStyle.Flex;
          _optionsScreenElement.SetEnabled(true);
        }
      }
      void BindExitButton()
      {
        var exitGame = _rootVisualElement.Q<Button>(_titleExitButtonName);
        if (exitGame != null)
        {
          exitGame.clickable.clicked += () =>
          {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
          };
        }
      }
    }

    private void BindOtherScreens()
    {
      _optionsScreenVE.InitAndBinds();
    }

    private void BindButtonsReturnToMainMenu()
    {
      BindOptionReturnToTitileButton();

      void BindOptionReturnToTitileButton()
      {
        var optionClose = _optionsScreenVE.Q<Button>(_optionsReturnToTitleButtonName);
        if (optionClose != null)
        {
          optionClose.clickable.clicked += () =>
          {
            HideOptions();
            ShowTitle();
          };
        }

        void HideOptions()
        {
          _optionsScreenElement.style.display = DisplayStyle.None;
          _optionsScreenElement.SetEnabled(false);
        }
      }
    }

    private void TransitionsAtStart()
    {
      StartCoroutine(AddTransitions());

      IEnumerator AddTransitions()
      {
        var titileGameLabel = _titleScreenElement.Q<VisualElement>(_titleGameLabelName);
        yield return new WaitForEndOfFrame();
        titileGameLabel.AddToClassList(_transitionOpacityOut);
        titileGameLabel?.RegisterCallback<TransitionEndEvent>(ShowTitle);
      }

      void ShowTitle(EventBase ev)
      {
        var titleButtonsPanel = _titleScreenElement.Q<VisualElement>(_titleButtonsPanel);
        titleButtonsPanel.AddToClassList(_transitionOpacityOut);
      }
    }

    private void ShowTitle()
    {
      _titleScreenElement.style.display = DisplayStyle.Flex;
      _titleScreenElement.SetEnabled(true);
    }

    private void HideTitle()
    {
      _titleScreenElement.style.display = DisplayStyle.None;
      _titleScreenElement.SetEnabled(false);
    }
  }
}
