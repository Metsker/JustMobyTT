namespace _Project.CodeBase.Core.Utils.Fsm
{
    public interface IPayloadedState<in T>
    {
        void OnEnter(T payload);
    }
}
