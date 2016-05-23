
namespace BehaviorEngine
{
    public interface INode
    {
        NodeState Update();
        void Start();
        void End();
    }
}