using System.Collections.Generic;
using _Project.CodeBase.Core.Data.Cubes;
using _Project.CodeBase.Core.Entities.Cubes.States;
using _Project.CodeBase.Core.Entities.Cubes.States.Draggable;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Base;
using _Project.CodeBase.Core.Entities.Cubes.Strategies.Raycast.Fallback;
using _Project.CodeBase.Core.Utils.Fsm;
using _Project.CodeBase.Core.Utils.Fsm.DraggableStateMachine;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes
{
    public interface ICubeFactory
    {
        Cube Create(int modelIndex);
        Cube Create(int modelIndex, RectTransform parent);
        IEnumerable<Cube> CreateFromModels(RectTransform parent);
        ICubeModelsProvider ModelsProvider { get; }
    }

    public class CubeFactory : ICubeFactory
    {
        [Inject] private ICubeModelsProvider _modelsProvider;
        
        public ICubeModelsProvider ModelsProvider => _modelsProvider;

        private readonly Cube _prefab;
        private readonly RectTransform _rootLayout;
        private readonly Container _container;

        public CubeFactory(
            Cube prefab,
            RectTransform rootLayout,
            Container container)
        {
            _container = container;
            _rootLayout = rootLayout;
            _prefab = prefab;
        }

        public Cube Create(int modelIndex)
        {
            return Create(modelIndex, _rootLayout);
        }

        public Cube Create(int modelIndex, RectTransform parent)
        {
            // TODO: object pooling
            Cube owner = Object.Instantiate(_prefab, parent);
            
            DraggableStateMachine fsm = InitializeStateMachine(owner);

            Sprite sprite = ModelsProvider.AtIndex(modelIndex).Sprite;
            
            owner.Construct(sprite, modelIndex, fsm);
            
            return owner;
        }

        public IEnumerable<Cube> CreateFromModels(RectTransform parent)
        {
            for (int i = 0; i < ModelsProvider.Count; i++)
                yield return Create(i, parent);
        }

        private DraggableStateMachine InitializeStateMachine(Cube owner)
        {
            PreviewState previewState = new (owner);
            AttributeInjector.Inject(previewState, _container);

            RaycastFallbackStrategy raycastFallbackStrategy = new (owner);
            AttributeInjector.Inject(raycastFallbackStrategy, _container);
            
            FloatingState floatingState = new (SelectRaycastStrategies(owner), raycastFallbackStrategy, owner);
            AttributeInjector.Inject(floatingState, _container);
            
            TowerState towerState = new (owner);
            AttributeInjector.Inject(towerState, _container);
            
            MissState missState = new (owner);
            AttributeInjector.Inject(missState, _container);
            
            HoleState holeState = new (owner);
            AttributeInjector.Inject(holeState, _container);
            
            FallingState fallingState = new (owner);
            AttributeInjector.Inject(fallingState, _container);
            
            return new DraggableStateMachine(new IState[]
            {
                previewState, floatingState, towerState, missState, holeState, fallingState
            });
        }
        
        private IEnumerable<IRaycastStrategy> SelectRaycastStrategies(Cube owner)
        {
            TowerRaycastStrategy towerRaycastStrategy = new (owner);
            AttributeInjector.Inject(towerRaycastStrategy, _container);
            yield return towerRaycastStrategy;
            
            TowerFoundationRaycastStrategy towerFoundationRaycastStrategy =  new (owner);
            AttributeInjector.Inject(towerFoundationRaycastStrategy, _container);
            yield return towerFoundationRaycastStrategy;
            
            HoleRaycastStrategy holeRaycastStategy = new (owner);
            AttributeInjector.Inject(holeRaycastStategy, _container);
            yield return holeRaycastStategy;
        }
    }
}
