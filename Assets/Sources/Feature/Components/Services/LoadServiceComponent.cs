using DrebotGS.Services;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Meta, Unique]
public sealed class LoadServiceComponent : IComponent
{
  public ILoadService instance;
}