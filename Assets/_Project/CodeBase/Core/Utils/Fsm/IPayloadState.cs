namespace _Project.CodeBase.Core.Utils.Fsm
{
    public interface IPayloadState<in T> : IState
    {
        void OnEnter(T payload);
    }
}
