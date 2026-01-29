using _Project.CodeBase.Core.Entities.Cubes.States;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Fallback.Base;
using _Project.CodeBase.Core.Logger;
using Reflex.Attributes;
using static _Project.CodeBase.Core.Logger.LocalizationKeys;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Fallback
{
    public class RaycastFallbackStrategy : CubeRaycastFallbackStrategyBase
    {
        [Inject] protected IActionLogger actionLogger;
        
        public RaycastFallbackStrategy(Cube owner) : base(owner)
        {
        }

        public override void Apply()
        {
            owner.Fsm.SetState<MissState>();
            actionLogger.Log(MissKey);
        }
    }
}
