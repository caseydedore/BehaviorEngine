using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class AdjustableResultNode : ANode
    {
        private NodeState statusOverride = NodeState.Inactive;
        private bool useStatusOverride = false;


        public AdjustableResultNode(NodeState defaultState)
        {
            Status = defaultState;
        }

        public override void Update()
        {
            if(useStatusOverride)
            {
                Status = statusOverride;
                useStatusOverride = false;
            }
        }

        public void SetStatusOnNextUpdate(NodeState status)
        {
            statusOverride = status;
            useStatusOverride = true;
        }
    }
}
