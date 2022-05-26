using DrebotGS;
using DrebotGS.Core;
using DrebotGS.Core.Loading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TempInput : MonoBehaviour
{
  public PlayerInput playerInput;

  private InputMasterControls _inputControl;

  // Inject
  private LoadingSceneHelper _loadingSceneHelper;

  [Inject]
  public void Inject(LoadingSceneHelper loadingSceneHelper)
  {
    _loadingSceneHelper = loadingSceneHelper;
  }

  void Awake()
  {
    if (playerInput == null)
      playerInput = gameObject.GetComponent<PlayerInput>();
    if (playerInput == null)
      return;

    _inputControl = new InputMasterControls();
    _inputControl.Enable();
    _inputControl.LoadingControls.Submit.performed += PlayerInput_onActionTriggered;
  }

  private void OnEnable()
  {
    _inputControl.Enable();
  }

  private void OnDisable()
  {
    _inputControl.Disable();
  }

  private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
  {
    _loadingSceneHelper.LoadMainMenuScene();

    _inputControl.Disable();
  }
}
