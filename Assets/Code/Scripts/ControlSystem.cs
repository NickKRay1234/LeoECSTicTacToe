using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe
{
    public class ControlSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        
        
        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var camera = _sceneData.Camera;
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo))
                {
                    var cellView = hitInfo.collider.GetComponent<CellView>();
                    if (cellView)
                    {
                        cellView.Entity.Get<Clicked>();
                    }
                }
            }
        }
    }

    internal struct Clicked
    {
        
    }
}