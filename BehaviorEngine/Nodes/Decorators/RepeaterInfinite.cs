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

        private NodeState Status { get; set; }


        public RepeaterInfinite(bool ignoreChildStatus = false)
        {
            this.ignoreChildStatus = ignoreChildStatus;
        }

        public override NodeState Update()
        {
            if (Child == null) return NodeState.Error;

            if (Status != NodeState.Active)
            {
                Child.Start();
            }

            Status = Child.Update();

            if (Status != NodeState.Active)
            {
                Child.End();

                if (ignoreChildStatus) return NodeState.Active;
            }

            if (ignoreChildStatus) return NodeState.Successful;

            return Status;
        }
    }
}
