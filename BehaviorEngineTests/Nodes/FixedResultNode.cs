using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class FixedResultNode : ANode
    {
        private NodeState ReturnState { get; set; }


        public FixedResultNode(NodeState returnState)
        {
            ReturnState = returnState;
        }

        public override NodeState Update()
        {
            return ReturnState;
        }
    }
}
