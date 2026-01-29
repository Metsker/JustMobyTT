namespace _Project.CodeBase.Core.Logger
{
    public interface IActionLogger
    {
        bool IsEnabled { get; set; }
        void Log(string text);
    }
}
