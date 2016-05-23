
namespace BehaviorEngine
{
    public interface INodeDecorator : INode
    {
        INode Child { get; }
    }
}