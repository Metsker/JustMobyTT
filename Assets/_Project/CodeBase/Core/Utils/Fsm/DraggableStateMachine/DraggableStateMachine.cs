using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.Core.Utils.Fsm.DraggableStateMachine
{
    public class DraggableStateMachine : StateMachine<IState>
    {
        public DraggableStateMachine(IEnumerable<IState> states) : base(states)
        {
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (currentState is IDraggableState draggableState)
                draggableState.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (currentState is IDraggableState draggableState)
                draggableState.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (currentState is IDraggableState draggableState)
                draggableState.OnEndDrag(eventData);
        }
    }
}
