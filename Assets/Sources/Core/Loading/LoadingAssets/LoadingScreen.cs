using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace DrebotGS.Core.Loading
{
  public class LoadingScreen : MonoBehaviour
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
      InitAfterInject();
    }

    void InitAfterInject()
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
      if(_inputControl != null)
      _inputControl.Enable();
    }

    private void OnDisable()
    {
      if (_inputControl != null)
        _inputControl.Disable();
    }
    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
      if (_contexts.gameState.loadingProviderEntity.isLoadedOperations)
        _contexts.gameState.loadingProviderEntity.isUnloadProvider = true;
    }

    public void OnProgress(float progress)
    {
      Debug.LogWarning("Progress loading: " + progress);
    }
  }
}