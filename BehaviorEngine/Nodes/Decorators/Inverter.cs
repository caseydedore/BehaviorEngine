
using System.Collections;
using System;

namespace BehaviorEngine
{
    public class Inverter : ANodeDecorator
    {
        public Inverter(ABehaviorEngine master) : base(master) { }

        private NodeState Status { get; set; }

        public override NodeState Update()
        {
            Status = Child.Update();
            if (Status == NodeState.Successful) return NodeState.Failure;
            else if (Status == NodeState.Failure) return NodeState.Successful;

            return Status;
        }
    }
}