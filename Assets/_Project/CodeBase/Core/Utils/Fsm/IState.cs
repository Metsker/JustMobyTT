namespace _Project.CodeBase.Core.Utils.Fsm
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}
