using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, GameState, Timer, Cleanup(CleanupMode.DestroyEntity), Event(EventTarget.Self)]
public sealed class DestroyedComponent : IComponent
{
}