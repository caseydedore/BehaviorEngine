using System;
using System.Collections;

namespace BehaviorEngine
{
    public abstract class ANodeTask : INode
    {
        public NodeState Status { get; protected set; }


        public virtual void Update(){}

        public virtual void Start()
        {
            Status = NodeState.Active;
        }

        public virtual void End()
        {
            Status = NodeState.Inactive;
        }
    }
}