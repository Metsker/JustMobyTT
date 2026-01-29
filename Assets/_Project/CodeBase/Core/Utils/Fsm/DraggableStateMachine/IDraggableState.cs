using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Utils.Fsm.DraggableStateMachine
{
    public interface IDraggableState : IState
    {
        void OnBeginDrag(PointerEventData eventData);
        void OnDrag(PointerEventData eventData);
        void OnEndDrag(PointerEventData eventData);
    }
}
