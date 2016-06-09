using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : ANode
    {
        public List<INode> Children { get; set; }

        public ANodeComposite()
        {
            Children = new List<INode>();
        }

        public override void Start()
        {
            Status = NodeState.Inactive;
        }

        public override void End()
        {
            foreach (var c in Children)
            {
                if(c.Status == NodeState.Active) c.End();
            }

            Status = NodeState.Inactive;
        }
    }
}
