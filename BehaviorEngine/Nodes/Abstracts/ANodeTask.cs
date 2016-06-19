using System;
using System.Collections;

namespace BehaviorEngine
{
    public abstract class ANodeTask : INode
    {
        public NodeState Status { get; protected set; }


        public virtual void Update(){}

        public abstract void Start();

        public abstract void End();
    }
}