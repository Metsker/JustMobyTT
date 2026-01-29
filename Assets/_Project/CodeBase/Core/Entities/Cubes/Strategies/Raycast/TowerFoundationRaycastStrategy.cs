using _Project.CodeBase.Core.Entities.Cubes.States;
using _Project.CodeBase.Core.Entities.Cubes.States.Draggable;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base;
using UnityEngine.EventSystems;
using static _Project.CodeBase.Core.Logger.LocalizationKeys;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast
{
    public class TowerFoundationRaycastStrategy : CubeRaycastStrategyBase
    {
        public TowerFoundationRaycastStrategy(Cube owner) : base(owner)
        {
        }

        public override bool TryApply(RaycastResult result)
        {
            if (result.gameObject.TryGetComponent(out TowerFoundation towerFoundation) &&
                owner.transform.position.y > towerFoundation.transform.position.y &&
                towerFoundation.Next == null)
            {
                towerFoundation.Next = owner;
                owner.Prev = towerFoundation;
                
                owner.Fsm.SetState<FallingState, FallingStatePayload>(new FallingStatePayload
                {
                    destinationY = towerFoundation.transform.position.y,
                    nextState = typeof(TowerState),
                    loggerKey = TowerPlacementFirstKey,
                });
                return true;
            }
            return false;
        }
    }
}
