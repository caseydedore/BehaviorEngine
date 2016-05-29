
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

        public override void Update()
        {
            Child.Update();
            Status = Child.Status;

            if (Status == NodeState.Successful) Status = successful;
            else if (Status == NodeState.Failure) Status = failure;
            else if (Status == NodeState.Active) Status = active;
        }
    }
}