using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeDecorator : ANode
    {
        protected ANode Child { get; set; }

        public void SetChild(ANode child)
        {
            child.AddStartEvent(Started);
            child.AddEndEvent(Ended);
            child.AddFinishedEvent(ChildFinished);

            Child = child;
        }

        protected override void EndRoutine()
        {
            Child.End();
        }

        protected abstract void ChildFinished(NodeState state);
    }
}
