using DrebotGS.Services;
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
    private SceneLoaderService _sceneLoaderService;

    [Inject]
    public void Inject(Contexts context, SceneLoaderService sceneLoaderService)
    {
      _contexts = context;
      _sceneLoaderService = sceneLoaderService;
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
     // _inputControl.LoadingControls.Continue.performed += PlayerInput_onActionTriggered;
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
      _ = _sceneLoaderService.UnloadProvider();
    }

    public void OnProgress(float progress)
    {
      Debug.LogWarning("Progress loading: " + progress);
    }
  }
}