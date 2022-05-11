using DrebotGS.Core.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TempInput : MonoBehaviour
{
  public PlayerInput playerInput;

  private Contexts _contexts;
  private LoadTitileSceneOperation _loadTitileSceneOperation;
  private LoadingSceneProvider _loadingSceneProvider;
  PlayerInputControls inputControl;

  [Inject]
  public void Inject(
      Contexts context,
      LoadTitileSceneOperation loadTitileSceneOperation,
      LoadingSceneProvider loadingSceneProvider)
  {
    _contexts = context;
    _loadTitileSceneOperation = loadTitileSceneOperation;
    _loadingSceneProvider = loadingSceneProvider;
  }

  void Start()
  {
    if (playerInput == null)
      playerInput = gameObject.GetComponent<PlayerInput>();
    if (playerInput == null)
      return;

    inputControl = new PlayerInputControls();
    inputControl.Enable();
    inputControl.Intro.Continue.performed += PlayerInput_onActionTriggered;
  }

  private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
  {
    LoadLoadingScene();

    inputControl.Disable();
  }

  private void LoadLoadingScene()
  {
    var loadingOperations = new Queue<ILoadingOperation>();
    loadingOperations.Enqueue(_loadTitileSceneOperation);
    _contexts.gameState.CreateLoadingProvider(_loadingSceneProvider, loadingOperations, false);
  }
}
