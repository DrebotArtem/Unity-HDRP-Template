using System;
using System.Collections.Generic;

namespace DrebotGS.StateMachine
{
  public class StateMachine : IStateMachine
  {
    private State _currentState;
    private Dictionary<Type, State> _states;

    public StateMachine()
    {
      _states = new Dictionary<Type, State>();
    }

    public void AddState<T>(State state)
    {
      var type = typeof(T);
      _states[type] = state;
    }

    public void SwitchState<T>() where T : State
    {
      var type = typeof(T);
      if (!_states.ContainsKey(type))
        return;

      _currentState?.Stop();
      _currentState = _states[type];
      _currentState.stateMachine = this;
      _states[type].Start();
    }

    public bool CompateState<T>() => _currentState?.GetType() == typeof(T);
    public T TakeState<T>() where T : State => _states[typeof(T)] as T;
  }
}