using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TempLoadingSceneInput : MonoBehaviour
{
  public PlayerInput playerInput;

  private InputMasterControls _inputControl;

  // Inject
  private Contexts _contexts;

  [Inject]
  public void Inject(
      Contexts context)
  {
    _contexts = context;
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
    _contexts.gameState.loadingProviderEntity.isUnloadProvider = true;
  }
}
