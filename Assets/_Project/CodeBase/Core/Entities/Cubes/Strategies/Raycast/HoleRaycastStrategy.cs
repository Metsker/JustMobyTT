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
                owner.Sm.SetState(new FallingState<HoleState>(
                    hole.transform.position.y, owner, () => actionLogger.Log(HoleDropKey)));
                return true;
            }
            return false;
        }
    }
}
