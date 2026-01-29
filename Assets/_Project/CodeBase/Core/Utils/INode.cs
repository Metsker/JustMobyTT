namespace _Project.CodeBase.Core.Utils
{
    public interface INode
    {
        INode Next { get; set; }
        INode Prev { get; set; }
    }
}
