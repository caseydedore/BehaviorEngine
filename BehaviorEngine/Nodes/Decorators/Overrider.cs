
using System.Collections;
using System;

namespace BehaviorEngine
{
    public class Overrider : ANodeDecorator
    {
        private NodeState successful,
                          active,
                          failure;


        public Overrider(NodeState successfulOverride = NodeState.Successful, NodeState activeOverride = NodeState.Active, 
            NodeState failureOverride = NodeState.Failure)
        {
            successful = successfulOverride;
            active = activeOverride;
            failure = failureOverride;
        }

        private NodeState Status { get; set; }

        public override NodeState Update()
        {
            Status = Child.Update();
            if (Status == NodeState.Successful) return successful;
            else if (Status == NodeState.Failure) return failure;
            else if (Status == NodeState.Active) return active;
            return Status;
        }
    }
}