
using System;

namespace BehaviorEngine
{
    public abstract class ANode : INode
    {
        public abstract NodeState Update();

        public virtual void Start() {}

        public virtual void End() {}
    }
}