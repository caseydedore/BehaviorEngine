using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeDecorator : INode
    {
        public NodeState Status { get; protected set; }

        public INode Child { get; set; }

        public abstract void Update();

        public virtual void Start()
        {
            Status = NodeState.Inactive;
        }

        public virtual void End()
        {
            if (Child != null && Child.Status == NodeState.Active) Child.End();
            Status = NodeState.Inactive;
        }
    }
}
