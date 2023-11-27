using Leopotam.Ecs;
using UnityEngine;

namespace TicTacToe 
{
    sealed class EcsStartup : MonoBehaviour 
    {
        EcsWorld _world;
        EcsSystems _systems;

        public Configuration Configuration;
        public SceneData SceneData;

        private void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            var gameState = new GameState();
            
            _systems
                .Add(new InitializeFieldSystem())
                .Add(new CreateCellViewSystem())
                .Add(new SetCameraSystem())
                .Add(new ControlSystem())
                .Add(new AnalyzeClickSystem())
                .Add(new CreateTakenViewSystem())
                
                .OneFrame<UpdateCameraEvent> ()
                .OneFrame<Clicked>()
                
                .Inject (Configuration)
                .Inject(SceneData)
                .Inject(gameState)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}