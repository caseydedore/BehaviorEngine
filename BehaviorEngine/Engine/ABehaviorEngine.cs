using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public abstract class ABehaviorEngine
    {
        protected INode BaseNode { get; set; }

        private NodeState Status { get; set; }


        protected void Update()
        {
            if(Status != NodeState.Active) BaseNode.Start();

            BaseNode.Update();
            Status = BaseNode.Status;

            if (Status != NodeState.Active) BaseNode.End();
        }
    }
}