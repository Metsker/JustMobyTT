using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public void SetState<TState>() where TState : T =>
            SetState(_states[typeof(TState)]);

        /*public void SetState<TState, TPayload>(TPayload payload) where TState : T, IPayloadState<TPayload>
        {
            currentState?.OnExit();
            currentState = _states[typeof(TState)];
            ((IPayloadState<TPayload>)currentState).OnEnter(payload);
        }*/

        public void SetState(T state)
        {
            currentState?.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}
