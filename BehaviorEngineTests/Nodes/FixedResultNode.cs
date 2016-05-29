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
        public FixedResultNode(NodeState returnState)
        {
            Status = returnState;
        }

        public override void Update() {}
    }
}
