using DrebotGS.Core.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Zenject;
using System;
using DrebotGS.Config;
using DrebotGS.Core;

namespace DrebotGS.UI
{
  [RequireComponent(typeof(UIDocument))]
  public class MainMenuScreenUI : MonoBehaviour
  {
    private VisualElement _rootVisualElement;
    private InputMasterControls _inputControl;

    //Inject
    private LoadingSceneHelper _loadingSceneHelper;

    [Inject]
    public void Inject(LoadingSceneHelper loadingSceneHelper)
    {
      _loadingSceneHelper = loadingSceneHelper;
    }

    public void Awake()
    {
      _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

      _inputControl = new InputMasterControls();
      _inputControl.Enable();

      BindMainMenuElements();
    }

    private void BindMainMenuElements()
    {
      var newGame = _rootVisualElement.Q<Button>("new-game-button");
      if (newGame != null)
      {
        newGame.clickable.clicked += () =>
        {
          NewGame();
        };
      }

      var optionsGame = _rootVisualElement.Q<Button>("options-button");
      if (optionsGame != null)
      {
        optionsGame.clickable.clicked += () =>
        {
          Options();
        };
      }

      var exitGame = _rootVisualElement.Q<Button>("exit-button");
      if (exitGame != null)
      {
        exitGame.clickable.clicked += () =>
        {
          Exit();
        };
      }
    }

    private void NewGame()
    {
      _loadingSceneHelper.LoadNewGameScene();
    }

    private void Options()
    {
      Debug.Log("Options");
    }

    private void Exit()
    {
      Application.Quit();
    }

    public void EnableGameScreen()
    {
      _rootVisualElement.style.display = DisplayStyle.Flex;
      _rootVisualElement.SetEnabled(true);
    }

    public void DisableGameScreen()
    {
      _rootVisualElement.style.display = DisplayStyle.None;
      _rootVisualElement.SetEnabled(false);
    }
  }
}
