using System;
using _Project.CodeBase.Core.Entities.Cubes.States.Base;
using _Project.CodeBase.Core.Utils;
using _Project.CodeBase.Core.Utils.Fsm;
using JetBrains.Annotations;
using PrimeTween;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes.States
{
    public struct FallingStatePayload
    {
        public Vector2 destination;
        [CanBeNull] public float? destinationY;
        
        public Type nextState;
        [CanBeNull] public string loggerKey;
    }

    public class FallingState : CubeStateBase, IPayloadedState<FallingStatePayload>
    {
        public FallingState(Cube owner) : base(owner)
        {
        }

        public void OnEnter(FallingStatePayload payload)
        {
            if (payload.destinationY != null)
                payload.destination = owner.transform.position.With(y: payload.destinationY);
            
            Debug.Assert(payload.nextState != null);
            
            Tween.PositionAtSpeed(
                    owner.transform,
                    payload.destination,
                    tweenSettingsLib.fallSpeed,
                    tweenSettingsLib.fallEase)
                .OnComplete(() =>
                {
                    if (!string.IsNullOrEmpty(payload.loggerKey))
                        actionLogger.Log(payload.loggerKey);
                    
                    owner.Fsm.SetState(payload.nextState);
                });
        }
    }
}
