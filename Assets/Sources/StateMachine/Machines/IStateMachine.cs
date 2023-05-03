namespace DrebotGS.StateMachine
{
  public interface IStateMachine
  {
    public void AddState<T>(State state);

    public void SwitchState<T>() where T : State;

    public bool CompateState<T>();

    public T TakeState<T>() where T : State;
  }
}