using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorEngine;

namespace BehaviorEngineTests.EngineImplementation
{
    public class Engine : ABehaviorEngine
    {
        new public void Update()
        {
            base.Update();
        }
    }
}
