using _Project.CodeBase.Core.Utils;
using _Project.CodeBase.Core.Utils.Fsm.DraggableStateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.CodeBase.Core.Entities.Cubes
{
    public class Cube : MonoBehaviour, INode, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image image;

        public DraggableStateMachine Sm => _stateMachine;
        public RectTransform RectTransform => (RectTransform)transform;
        public Bounds WorldBounds => CalcUnscaledWorldBounds();
        public int SpriteIndex { get; private set; }

        public INode Next { get; set; }
        public INode Prev { get; set; }

        private DraggableStateMachine _stateMachine;

        public void Construct(Sprite sprite, int spriteIndex, DraggableStateMachine stateMachine)
        {
            image.sprite = sprite;
            SpriteIndex = spriteIndex;
            _stateMachine = stateMachine;
        }

        public void OnBeginDrag(PointerEventData eventData) =>
            _stateMachine.OnBeginDrag(eventData);

        public void OnDrag(PointerEventData eventData) =>
            _stateMachine.OnDrag(eventData);

        public void OnEndDrag(PointerEventData eventData) =>
            _stateMachine.OnEndDrag(eventData);

        public bool IntersectsX(Cube other, float sizeMult = .5f)
        {
            Bounds bounds = WorldBounds;
            bounds.center = bounds.center.With(y: other.WorldBounds.center.y);
            bounds.size *= sizeMult;
            
            return other.WorldBounds.Intersects(bounds);
        }

        private Bounds CalcUnscaledWorldBounds()
        {
            Vector3[] worldCorners = new Vector3[4];
            RectTransform.GetWorldCorners(worldCorners);
                    
            Vector3 min = worldCorners[0];
            Vector3 max = worldCorners[2];

            Vector3 size = max - min;
            size.x /= transform.localScale.x;
            size.y /= transform.localScale.y;
            
            return new Bounds((min + max) / 2, size);
        }
    }
}
