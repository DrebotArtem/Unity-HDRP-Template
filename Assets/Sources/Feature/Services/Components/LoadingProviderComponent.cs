using DrebotGS.Core.Loading;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

[GameState, Unique]
public sealed class LoadingProviderComponent : IComponent
{
    public ILoadingProvider provider;
    public Queue<ILoadingOperation> loadingOperations;
}