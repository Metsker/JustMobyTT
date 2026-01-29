using _Project.CodeBase.Core.Utils.Fsm.DraggableStateMachine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Base
{
    public abstract class CubeDraggableStateBase : CubeStateBase, IDraggableState
    {
        protected CubeDraggableStateBase(Cube owner) : base(owner)
        {
        }
        
        public virtual void OnBeginDrag(PointerEventData eventData) {}

        public virtual void OnDrag(PointerEventData eventData) {}

        public virtual void OnEndDrag(PointerEventData eventData) {}
    }
}
