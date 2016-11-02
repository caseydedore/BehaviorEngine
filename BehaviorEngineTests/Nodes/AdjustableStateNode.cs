using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class AdjustableStateNode : ANode
    {
        public NodeState Status { get; protected set; }

        private NodeState statusOverride = NodeState.Inactive;
        private bool useStatusOverride = false;


        public AdjustableStateNode(NodeState defaultState)
        {
            Status = defaultState;
        }

        protected override NodeState UpdateRoutine()
        {
            if (useStatusOverride)
            {
                Status = statusOverride;
                useStatusOverride = false;
            }

            return Status;
        }

        public void SetStatusOnNextUpdate(NodeState status)
        {
            statusOverride = status;
            useStatusOverride = true;
        }
    }
}
