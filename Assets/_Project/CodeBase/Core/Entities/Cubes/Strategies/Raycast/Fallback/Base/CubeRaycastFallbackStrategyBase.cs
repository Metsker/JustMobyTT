namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Fallback.Base
{
    public abstract class CubeRaycastFallbackStrategyBase : IRaycastFallbackStrategy
    {
        protected readonly Cube owner;

        protected CubeRaycastFallbackStrategyBase(Cube owner)
        {
            this.owner = owner;
        }

        public abstract void Apply();
    }
}
