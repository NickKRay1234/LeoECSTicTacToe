using Leopotam.Ecs;
using UnityEngine;

namespace TicTacToe {
    internal class InitializeFieldSystem : IEcsInitSystem {
        
        private Configuration _configuration;
        private EcsWorld _world;
        
        public void Init () 
        {
            for (int x = 0; x < _configuration.LevelWidth; x++)
            {
                for (int y = 0; y < _configuration.LevelHeight; y++)
                {
                    var cellEntity = _world.NewEntity();
                    cellEntity.Get<Cell>();
                    cellEntity.Get<Position>().value = new Vector2Int(x,y);
                }
            }
            _world.NewEntity().Get<UpdateCameraEvent>();
        }
    }
}