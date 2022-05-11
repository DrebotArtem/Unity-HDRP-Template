using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TempLoadingSceneInput : MonoBehaviour
{
  public PlayerInput playerInput;

  private Contexts _contexts;
  PlayerInputControls inputControl;

  [Inject]
  public void Inject(
      Contexts context)
  {
    _contexts = context;
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
    _contexts.gameState.loadingProviderEntity.isUnloadProvider = true;
  }
}
