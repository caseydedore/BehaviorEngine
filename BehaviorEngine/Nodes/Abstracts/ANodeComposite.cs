using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : ANode
    {
        protected List<ANode> Children { get; set; }


        public ANodeComposite()
        {
            Children = new List<ANode>();
        }

        public void AddChild(ANode child)
        {
            child.AddStartEvent(Started);
            child.AddEndEvent(Ended);
            child.AddFinishedEvent(ChildFinished);

            Children.Add(child);
        }

        protected override void EndRoutine()
        {
            foreach (var c in Children)
            {
                c.End();
            }
        }

        protected abstract void ChildFinished(NodeState state);
    }
}
