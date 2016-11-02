using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests
{
    [TestClass]
    public class BehaviorEngineTests
    {
        [TestMethod]
        public void SetRootChild()
        {
            var engine = new BehaviorEngineTest();
            var child = new EventTrackingNode(NodeState.Successful);

            engine.SetRootChild(child);

            Assert.AreSame(child, engine.RootChild);
        }
    }
}
