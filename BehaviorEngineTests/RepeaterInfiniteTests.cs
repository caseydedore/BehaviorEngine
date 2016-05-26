using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorEngineTests
{
    [TestClass]
    public class RepeaterInfiniteTests
    {
        [TestMethod]
        public void RepeatWithChildSuccess()
        {
            RepeatWithChildReturn(NodeState.Successful);
        }

        [TestMethod]
        public void RepeatWithChildFailure()
        {
            RepeatWithChildReturn(NodeState.Failure);
        }

        [TestMethod]
        public void RepeatWithChildError()
        {
            RepeatWithChildReturn(NodeState.Error);
        }

        [TestMethod]
        public void RepeatWithChildActive()
        {
            RepeatWithChildReturn(NodeState.Active);
        }


        private void RepeatWithChildReturn(NodeState childStatus)
        {
            var repeater = new RepeaterInfinite();
            repeater.Child = new FixedResultNode(childStatus);
            var status = NodeState.Error;
            var count = 1;

            repeater.Start();

            for(; count <= 1000; count++)
            {
                status = repeater.Update();
                if (status != NodeState.Active) break;
            }

            Assert.AreEqual(count, 1000);
        }
    }
}
