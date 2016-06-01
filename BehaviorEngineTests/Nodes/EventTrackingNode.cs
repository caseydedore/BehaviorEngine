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

        public int StartsTotal { get; protected set; }
        public int EndsTotal { get; protected set; }
        public int UpdatesTotal { get; protected set; }
        public int UpdatesCurrent { get; protected set; }


        public EventTrackingNode(NodeState defaultState) : base(defaultState) { }

        public override void Start()
        {
            hasStarted = true;
            StartsTotal++;
        }

        public override void End()
        {
            hasEnded = true;
            EndsTotal++;
            UpdatesCurrent = 0;
        }

        public override void Update()
        {
            base.Update();
            UpdatesTotal++;
            UpdatesCurrent++;
        }
    }
}
