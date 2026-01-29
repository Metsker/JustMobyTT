using _Project.CodeBase.Core.Entities.Cubes.States;
using _Project.CodeBase.Core.Entities.Cubes.States.Draggable;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using static _Project.CodeBase.Core.Logger.LocalizationKeys;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast
{
    public class TowerRaycastStrategy : CubeRaycastStrategyBase
    {
        [Inject] protected Canvas canvas;
        
        public TowerRaycastStrategy(Cube owner) : base(owner)
        {
        }

        public override bool TryApply(RaycastResult result)
        {
            if (result.gameObject.TryGetComponent(out Cube cube) &&
                cube.Fsm.CurrentState is TowerState &&
                cube.Next == null &&
                owner.transform.position.y > cube.transform.position.y + cube.WorldBounds.size.y)
            {
                if (result.screenPosition.y + cube.RectTransform.rect.height > Screen.height)
                {
                    actionLogger.Log(HeightLimitKey);
                    owner.Fsm.SetState<MissState>();
                    return true;
                }
                
                Transform hit = result.gameObject.transform;

                Cube cubeHit = hit.GetComponent<Cube>();

                cubeHit.Next = owner;
                owner.Prev = cubeHit;

                Vector2 pos = AdjustedDestination(cubeHit, hit);
                
                owner.Fsm.SetState<FallingState, FallingStatePayload>(new FallingStatePayload
                {
                    destination = pos,
                    nextState = typeof(TowerState),
                    loggerKey = TowerPlacementKey,
                });
                return true;
            }
            return false;
        }

        private Vector2 AdjustedDestination(Cube cubeHit, Transform hit)
        {
            float posY = hit.position.y + owner.WorldBounds.size.y;
            float extX = cubeHit.WorldBounds.extents.x;
            float posX = hit.position.x;

            if (hit.position.x < owner.transform.position.x)
                posX += Random.Range(0, extX);
            else
                posX += Random.Range(-extX, 0);
            
            return new Vector2(posX, posY);
        }
    }
}
