using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base
{
    public interface IRaycastStrategy
    {
        bool TryApply(RaycastResult result);
    }
}
