using System;
using System.Collections;

namespace BehaviorEngine
{
    public abstract class ANodeTask : INode
    {
        private ABehaviorEngine Master { get; set; }

        public NodeState Status { get; protected set; }


        public ANodeTask(ABehaviorEngine master)
        {
            //potentially obsolete: custom ctors could simply pass the dependencies they actually need (As in the prototype).
            Master = master;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public abstract void Start();

        public abstract void End();
    }
}