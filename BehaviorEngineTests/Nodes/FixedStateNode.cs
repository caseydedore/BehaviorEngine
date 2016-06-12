using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class FixedStateNode : INode
    {
        public NodeState Status { get; protected set; }


        public FixedStateNode(NodeState returnState)
        {
            Status = returnState;
        }

        public void Update() {}

        public void Start() { }

        public void End() { }
    }
}
