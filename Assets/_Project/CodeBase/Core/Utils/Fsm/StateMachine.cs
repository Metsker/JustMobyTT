using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.CodeBase.Core.Utils.Fsm
{
    public class StateMachine<T> where T : IState
    {
        private readonly Dictionary<Type, T> _states;

        public T CurrentState => currentState;
        protected T currentState;

        public StateMachine(IEnumerable<T> states)
        {
            _states = states.ToDictionary(state => state.GetType());
        }
        
        public void SetState<TState>() where TState : T => SetState(typeof(TState));

        public void SetState<TState, TPayload>(TPayload payload) where TState : T, IPayloadedState<TPayload>
        {
            SetState<TState>();
            
            if (currentState is IPayloadedState<TPayload> payloadedState)
                payloadedState.OnEnter(payload);
            
        }

        public void SetState(Type stateType)
        {
            Debug.Assert(typeof(T).IsAssignableFrom(stateType));
            
            currentState?.OnExit();
            
            if (_states.TryGetValue(stateType, out T state))
            {
                currentState = state;
                currentState.OnEnter();
            }
            else
                Debug.LogError("No state with type " + stateType);
        }
    }
}
