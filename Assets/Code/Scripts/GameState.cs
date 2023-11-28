using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace TicTacToe
{
    public class GameState
    {
        public SignType CurrentType = SignType.Ring;
        public readonly Dictionary<Vector2Int, EcsEntity> Cells = new();
    }
}