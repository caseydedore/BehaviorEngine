
using System;

namespace BehaviorEngine
{
    public abstract class ANode : INode
    {
        public ABehaviorEngine Master { get; private set; }

        public ANode(ABehaviorEngine master)
        {
            Master = master;
        }

        public abstract NodeState Update();

        public virtual void Start()
        {

        }

        public virtual void End()
        {
            
        }
    }
}