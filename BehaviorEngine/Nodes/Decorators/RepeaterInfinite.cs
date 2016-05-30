using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngine
{ 

    public class RepeaterInfinite : ANodeDecorator
    {
        private bool ignoreChildStatus = false;

        private NodeState childStatus = NodeState.Inactive;


        public RepeaterInfinite(bool ignoreChildStatus = false)
        {
            this.ignoreChildStatus = ignoreChildStatus;
        }

        public override void Update()
        {
            if (Child == null)
            {
                Status = NodeState.Error;
                return;
            }

            if (childStatus != NodeState.Active)
            {
                Child.Start();
            }

            Child.Update();
            childStatus = Child.Status;
            Status = childStatus;

            if (Status != NodeState.Active)
            {
                Child.End();

                if (ignoreChildStatus) Status = NodeState.Active;
            }
        }

        public override void Start()
        {
            Status = NodeState.Inactive;
            childStatus = NodeState.Inactive;
        }

        public override void End()
        {
            Status = NodeState.Inactive;
            childStatus = NodeState.Inactive;
            Child.End();
        }
    }
}
