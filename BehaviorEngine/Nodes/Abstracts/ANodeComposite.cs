using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : ANode
    {
        private List<ANode> Children { get; set; }


        public ANodeComposite()
        {
            Children = new List<ANode>();
        }

        public void AddChild(ANode child)
        {
            child.AddStartEvent(new NodeEvent(ChildStartEvent));
            child.AddEndNodeEvent(new NodeEvent(ChildEndEvent));
            Children.Add(child);
        }

        public override void EndNode()
        {
            base.End();
            foreach (var c in Children)
            {
                if(c.Status == NodeState.Active) c.End();
            }
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
