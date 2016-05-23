using System;
using System.Collections;

namespace BehaviorEngine
{
    public abstract class ANodeTask : INode
    {
        private ABehaviorEngine Master { get; set; }

        protected NodeState Status { get; set; }


        public ANodeTask(ABehaviorEngine master)
        {
            //potentially obsolete: custom ctors could simply pass the dependencies they actually need (As in the prototype).
            Master = master;
        }

        public NodeState Update()
        {
            return Status;
        }

        public abstract void Start();

        public abstract void End();
    }
}