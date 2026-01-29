using _Project.CodeBase.Core.Logger;
using Reflex.Attributes;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base
{
    public abstract class CubeRaycastStrategyBase : IRaycastStrategy
    {
        [Inject] protected IActionLogger actionLogger;
        
        protected readonly Cube owner;

        protected CubeRaycastStrategyBase(Cube owner)
        {
            this.owner = owner;
        }

        public abstract bool TryApply(RaycastResult result);
    }
}
