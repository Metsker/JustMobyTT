using System.Collections.Generic;
using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Fallback.Base;
using PrimeTween;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Draggable
{
    public class FloatingState : CubeDraggableStateBase
    {
        private const float Step = 20f;

        [Inject] private Canvas _canvas;
        [Inject] private GraphicRaycaster _raycaster;
        
        private readonly IEnumerable<IRaycastStrategy> _raycastStrategies;
        private readonly IRaycastFallbackStrategy _raycastFallbackStrategy;

        public FloatingState(
            IEnumerable<IRaycastStrategy> raycastStrategies,
            IRaycastFallbackStrategy raycastFallbackStrategy,
            Cube owner) : base(owner)
        {
            _raycastFallbackStrategy = raycastFallbackStrategy;
            _raycastStrategies = raycastStrategies;
        }

        public override void OnEnter()
        {
            owner.transform.SetAsLastSibling();
            
            Tween.Scale(owner.transform, tweenSettingsLib.scaleInSettings);
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            owner.RectTransform.anchoredPosition = eventData.position / _canvas.scaleFactor;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            Tween.Scale(owner.transform, tweenSettingsLib.scaleOutSettings);
            
            List<RaycastResult> results = Raycast(eventData.position, Vector2.down, eventData.position.y, Screen.height / Step);
            ApplyStrategies(results);
        }

        private List<RaycastResult> Raycast(Vector2 position, Vector2 direction, float distance, float step)
        {
            var results = new List<RaycastResult>();

            while (distance > 0)
            {
                var data = new PointerEventData(EventSystem.current);
                data.position = position;

                _raycaster.Raycast(data, results);

                distance -= step;
                position += direction * step;
            }
            return results;
        }

        private void ApplyStrategies(List<RaycastResult> results)
        {
            foreach (RaycastResult result in results)
            {
                foreach (IRaycastStrategy strategy in _raycastStrategies)
                {
                    if (strategy.TryApply(result))
                        return;
                }
            }
            _raycastFallbackStrategy.Apply();
        }
    }
}
