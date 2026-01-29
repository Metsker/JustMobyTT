using System;
using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using _Project.CodeBase.Core.Utils;
using _Project.CodeBase.Core.Utils.Fsm;
using PrimeTween;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes.States
{
    public class FallingState<TNextState> : CubeStateBase where TNextState : IState
    {
        private readonly Vector3 _destination;
        private readonly Action _onExit;

        public FallingState(Vector2 destination, Cube owner, Action onExit = null) : base(owner)
        {
            _destination = destination;
            _onExit = onExit;
        }
        
        public FallingState(float destinationY, Cube owner, Action onExit = null) : base(owner)
        {
            _destination = owner.transform.position.With(y: destinationY);
            _onExit = onExit;
        }

        public override void OnEnter()
        {
            Tween.PositionAtSpeed(
                    owner.transform,
                    _destination,
                    tweenSettingsLib.fallSpeed,
                    tweenSettingsLib.fallEase)
                .OnComplete(owner.Sm.SetState<TNextState>);
        }
        
        public override void OnExit() =>
            _onExit?.Invoke();
    }
}
