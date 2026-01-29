using _Project.CodeBase.Core.Entities.Cubes.States;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base;
using UnityEngine.EventSystems;
using static _Project.CodeBase.Core.Logger.LocalizationKeys;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast
{
    public class HoleRaycastStrategy : CubeRaycastStrategyBase
    {
        public HoleRaycastStrategy(Cube owner) : base(owner)
        {
        }

        public override bool TryApply(RaycastResult result)
        {
            if (result.gameObject.TryGetComponent(out Hole hole))
            {
                owner.Fsm.SetState<FallingState, FallingStatePayload>(new FallingStatePayload
                {
                    destinationY = hole.transform.position.y,
                    nextState = typeof(HoleState),
                    loggerKey = HoleDropKey,
                });
                return true;
            }
            return false;
        }
    }
}
