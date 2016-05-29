
using System;

namespace BehaviorEngine
{
    public abstract class ANode : INode
    {
        public NodeState Status { get; protected set; }

        public abstract void Update();

        public virtual void Start() {}

        public virtual void End() {}
    }
}