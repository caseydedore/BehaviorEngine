using BehaviorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests.Nodes
{
    public class BehaviorEngineTest : ABehaviorEngine
    {
        public ANode RootChild { get { return Child; } }

        public void Update()
        {
            foreach(var c in RunningNodes)
            {
                c.Update();
            }
        }
    }
}
