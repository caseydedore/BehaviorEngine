
using System.Collections;
using System;

namespace BehaviorEngine
{
    public class Overrider : ANodeDecorator
    {
        public NodeState SuccessOverride { get; set; }
        public NodeState ActiveOverride { get; set; }
        public NodeState FailureOverride { get; set; }

        private NodeState childStatus = NodeState.Inactive;


        public override void Update()
        {
            if(Child == null)
            {
                Status = NodeState.Error;
                return;
            }

            if (childStatus != NodeState.Active) Child.Start();

            Child.Update();
            childStatus = Child.Status;
            Status = childStatus;

            if (Status == NodeState.Successful) Status = SuccessOverride;
            else if (Status == NodeState.Failure) Status = FailureOverride;
            else if (Status == NodeState.Active) Status = ActiveOverride;

            if (childStatus != NodeState.Active || Status != NodeState.Active) Child.End();
        }

        public override void Start()
        {
            childStatus = NodeState.Inactive;
        }

        public override void End()
        {
            if (Child.Status == NodeState.Active) Child.End();
        }
    }
}