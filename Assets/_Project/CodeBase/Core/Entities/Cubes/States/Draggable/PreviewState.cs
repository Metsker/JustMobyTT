using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Draggable
{
    public class PreviewState : CubeDraggableStateBase
    {
        private const float DragDotThreshold = 0.35f;
        
        [Inject] private ScrollRect _scroll;
        [Inject] private ICubeFactory _factory;

        private bool _passDrag;
        private Cube copy;

        public PreviewState(Cube owner) : base(owner)
        {
        }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 dragDirection = (eventData.position - eventData.pressPosition).normalized;
            _passDrag = Vector2.Dot(dragDirection, Vector2.up) < DragDotThreshold;
            
            if (_passDrag)
                _scroll.OnBeginDrag(eventData);
            else
                CreateCopy(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (_passDrag)
                _scroll.OnDrag(eventData);
            else
                copy.Fsm.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (_passDrag)
                _scroll.OnEndDrag(eventData);
            else
                copy.Fsm.OnEndDrag(eventData);
        }

        private void CreateCopy(PointerEventData eventData)
        {
            copy = _factory.Create(owner.SpriteIndex);
            copy.Fsm.SetState<FloatingState>();
            copy.Fsm.OnBeginDrag(eventData);
        }
    }
}
