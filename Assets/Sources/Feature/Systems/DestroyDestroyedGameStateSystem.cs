using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrebotGS.Systems
{
    public class DestroyDestroyedGameStateSystem : ICleanupSystem
    {

        readonly IGroup<GameStateEntity> _group;
        readonly List<GameStateEntity> _buffer = new List<GameStateEntity>();

        public DestroyDestroyedGameStateSystem(Contexts contexts)
        {
            _group = contexts.gameState.GetGroup(GameStateMatcher.Destroyed);
        }

        public void Cleanup()
        {
            foreach (var e in _group.GetEntities(_buffer))
            {
                e.Destroy();
            }
        }
    }
}