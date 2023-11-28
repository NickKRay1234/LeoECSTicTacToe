using System;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TicTacToe
{
    public class AnalyzeClickSystem : IEcsRunSystem
    {
        private EcsFilter<Cell, Clicked>.Exclude<Taken> _filter;
        private GameState _gameState;
        private SceneData _sceneData;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var ecsEntity = ref _filter.GetEntity(index);
                ecsEntity.Get<Taken>().value = _gameState.CurrentType;
                ecsEntity.Get<CheckWinEvent>();
                _gameState.CurrentType = _gameState.CurrentType == SignType.Cross ? SignType.Ring : SignType.Cross;
                _sceneData.UI.GameHUD.SetTurn(_gameState.CurrentType);
            }
        }
    }

    public class CreateTakenViewSystem : IEcsRunSystem
    {
        private EcsFilter<Taken, CellViewRef>.Exclude<TakenRef> _filter;
        private Configuration _configuration;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var position = _filter.Get2(index).value.transform.position;
                var takenType = _filter.Get1(index).value;
                SignView signView = null;
                switch (takenType)
                {
                    case SignType.Cross:
                        signView = _configuration.CrossView;
                        break;
                    case SignType.Ring:
                        signView = _configuration.RingView;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var instance = Object.Instantiate(signView, position, Quaternion.identity);
                _filter.GetEntity(index).Get<TakenRef>().value = instance;
            }
        }
    }
}