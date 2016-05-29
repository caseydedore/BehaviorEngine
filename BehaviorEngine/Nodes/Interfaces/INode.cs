
namespace BehaviorEngine
{
    public interface INode
    {
        NodeState Status { get; }

        void Update();
        void Start();
        void End();
    }
}