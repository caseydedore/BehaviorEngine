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


        public RepeaterInfinite(bool ignoreChildStatus = false)
        {
            this.ignoreChildStatus = ignoreChildStatus;
        }

        public override void Update()
        {
            if (Child == null) Status = NodeState.Error;

            if (Status != NodeState.Active)
            {
                Child.Start();
            }

            Child.Update();
            Status = Child.Status;

            if (Status != NodeState.Active)
            {
                Child.End();

                if (ignoreChildStatus) Status = NodeState.Active;
            }

            if (ignoreChildStatus) Status = NodeState.Successful;
        }

        public override void End()
        {
            Status = NodeState.Inactive;
            Child.End();
        }
    }
}
