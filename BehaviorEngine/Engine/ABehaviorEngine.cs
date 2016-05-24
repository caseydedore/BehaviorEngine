using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public abstract class ABehaviorEngine
    {
        protected INode BaseNode { get; set; }

        private NodeState status = NodeState.Successful;


        protected void Update()
        {
            if(status != NodeState.Active) BaseNode.Start();

            status = BaseNode.Update();

            if (status != NodeState.Active) BaseNode.End();
        }
    }
}