using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeDecorator : ANode
    {
        public INode Child { get; set; }

        public override void Start()
        {
            Status = NodeState.Inactive;
        }

        public override void End()
        {
            if (Child != null && Child.Status == NodeState.Active) Child.End();
            Status = NodeState.Inactive;
        }
    }
}
