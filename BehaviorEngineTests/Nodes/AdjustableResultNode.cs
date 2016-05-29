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
        new public NodeState Status { get; set; }


        public override void Update() {}
    }
}
