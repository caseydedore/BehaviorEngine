
using System.Collections;
using System;

namespace BehaviorEngine
{
    public class Overrider : ANodeDecorator
    {
        public NodeState SuccessOverride { get; set; }
        public NodeState ActiveOverride { get; set; }
        public NodeState FailureOverride { get; set; }


        public override void Update()
        {
            Child.Update();
            Status = Child.Status;

            if (Status == NodeState.Successful) Status = SuccessOverride;
            else if (Status == NodeState.Failure) Status = FailureOverride;
            else if (Status == NodeState.Active) Status = ActiveOverride;
        }
    }
}