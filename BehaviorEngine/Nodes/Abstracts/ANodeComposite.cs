using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : INode
    {
        public NodeState Status { get; protected set; }

        public List<INode> Children { get; set; }


        public ANodeComposite()
        {
            Children = new List<INode>();
        }

        public abstract void Update();

        public virtual void Start()
        {
            Status = NodeState.Active;
        }

        public virtual void End()
        {
            foreach (var c in Children)
            {
                if(c.Status == NodeState.Active) c.End();
            }

            Status = NodeState.Inactive;
        }

        protected int GetNumberOfChildrenWithStatus(NodeState status)
        {
            var number = 0;

            foreach (var c in Children)
            {
                if (c.Status == status) number++;
            }

            return number;
        }
    }
}
