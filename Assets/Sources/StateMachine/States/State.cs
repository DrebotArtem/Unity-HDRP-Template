namespace DrebotGS.StateMachine
{
  public abstract class State
  {
    public IStateMachine stateMachine { get; set; }
    public virtual void Start() { }
    public virtual void Stop() { }
  }
}