using _Project.CodeBase.Core.Utils.Fsm;
using UnityEngine;

namespace _Project.CodeBase.Core.Entities.Cubes.States.Base
{
    public abstract class MonoStateBase<TOwner> : IState where TOwner : MonoBehaviour
    {
        protected readonly TOwner owner;

        protected MonoStateBase(TOwner owner)
        {
            this.owner = owner;
        }
        
        public virtual void OnEnter() {}

        public virtual void OnExit() {}
    }
}
