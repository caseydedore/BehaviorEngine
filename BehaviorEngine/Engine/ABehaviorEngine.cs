using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public abstract class ABehaviorEngine
    {
        protected INode BaseNode { get; set; }

        protected void Update()
        {
            if (BaseNode.Update() != NodeState.Active)
            {
                BaseNode.End();
                BaseNode.Start();
            }
        }
    }
}