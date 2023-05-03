using DrebotGS.Core.Loading;
using DrebotGS.StateMachine.Loading;
using System.Threading.Tasks;

namespace DrebotGS.Services
{
  public class SceneLoaderService
  {
    private StateMachine.StateMachine _stateMachine;
    private ILoadingProvider _loadingProvider;

    public SceneLoaderService()
    {
      _stateMachine = new StateMachine.StateMachine();
      InitializationSates();
    }

    private void InitializationSates()
    {
      var doneState = new DoneState();
      var progressState = new ProgressState();
      var waitUploadProviderState = new WaitUploadProviderState();

      _stateMachine.AddState<DoneState>(doneState);
      _stateMachine.AddState<ProgressState>(progressState);
      _stateMachine.AddState<WaitUploadProviderState>(waitUploadProviderState);
      _stateMachine.SwitchState<DoneState>();
    }

    public async void LoadProvider(ILoadingProvider loadingProvider, bool unloadProviderAfterLoad = true)
    {
      if (!ProviderIsReady()) return;

      UnloadOldOperations();
      SetNewLoadingProvider();
      await LoadProvider();
      await LoadOperations();
      await TryUnloadProvider();

      bool ProviderIsReady()
      {
        return _stateMachine.CompateState<DoneState>();
      }

      void SetNewLoadingProvider()
      {
        if (unloadProviderAfterLoad)
          _stateMachine.SwitchState<WaitUploadProviderState>();

        _loadingProvider = loadingProvider;
      }
    }

    public async Task UnloadProvider()
    {
      if (_stateMachine.CompateState<DoneState>())
        return;

      await _loadingProvider.UnloadProvider();
      _stateMachine.SwitchState<DoneState>();
    }

    private void UnloadOldOperations()
    {
      _loadingProvider?.UnloadOperations();
    }

    private async Task LoadProvider()
    {
      _stateMachine.SwitchState<ProgressState>();
      await _loadingProvider.LoadProvider();
    }

    private async Task LoadOperations()
    {
      await _loadingProvider.LoadOperations();
    }

    private async Task TryUnloadProvider()
    {
      if (!_stateMachine.CompateState<WaitUploadProviderState>())
        await UnloadProvider();
    }
  }
}