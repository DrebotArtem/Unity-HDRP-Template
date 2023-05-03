using DrebotGS.Config;
using DrebotGS.Core.Loading;
using DrebotGS.Factories;
using DrebotGS.Services;
using DrebotGS.StateMachine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace DrebotGS.StateMachine
{
  public class IntroState : State
  {
    private readonly GameScenesCatalogue _gameScenesCatalogue;
    private readonly LoadingProvidersFactory _loadingProvidersFactory;
    private readonly SceneLoaderService _sceneLoaderService;
    private InputMasterControls _inputControl;

    public IntroState(GameScenesCatalogue gameScenesCatalogue,
      LoadingProvidersFactory loadingProvidersFactory,
      SceneLoaderService sceneLoaderService)
    {
      _gameScenesCatalogue = gameScenesCatalogue;
      _loadingProvidersFactory = loadingProvidersFactory;
      _sceneLoaderService = sceneLoaderService;
    }

    public override void Start()
    {
      LoadIntroScene();

      _inputControl = new InputMasterControls();
      _inputControl.Enable();
      _inputControl.LoadingControls.Submit.performed += OnContinueTriggered;
    }

    public override void Stop()
    {
      base.Stop();
      _inputControl.LoadingControls.Submit.performed -= OnContinueTriggered;
    }

    private void LoadIntroScene()
    {
      var loadingOperations = new Queue<ILoadingOperation>();
      loadingOperations.Enqueue(new LoadSceneOperation(_gameScenesCatalogue.IntroScene));
      var emptyProvider = _loadingProvidersFactory.CreateEmptyProvider(loadingOperations);
      _sceneLoaderService.LoadProvider(emptyProvider);
    }

    private void OnContinueTriggered(InputAction.CallbackContext context)
    {
      if (context.performed)
        stateMachine.SwitchState<MainMenuState>();
    }
  }
}