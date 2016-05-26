using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    class EventTrackingNode : AdjustableResultNode
    {
        public bool HasStarted { get { return hasStarted; } }
        private bool hasStarted = false;
        public bool HasEnded { get { return hasEnded; } }
        private bool hasEnded = false;


        public override void Start()
        {
            hasStarted = true;
        }

        public override void End()
        {
            hasEnded = true;
        }
    }
}
