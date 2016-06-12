using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class AdjustableStateNode : INode
    {
        public NodeState Status { get; protected set; }

        private NodeState statusOverride = NodeState.Inactive;
        private bool useStatusOverride = false;


        public AdjustableStateNode(NodeState defaultState)
        {
            Status = defaultState;
        }

        public virtual void Update()
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

        public virtual void Start() { }

        public virtual void End() { }
    }
}
