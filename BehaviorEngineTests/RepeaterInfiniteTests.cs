using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorEngineTests
{
    [TestClass]
    public class RepeaterInfiniteTests
    {
        [TestMethod]
        public void UpdateStartsChild()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(true, childNode.HasStarted);
        }

        [TestMethod]
        public void EndAfterStartEndsChild()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.End();

            Assert.AreEqual(true, childNode.HasEnded);
        }

        [TestMethod]
        public void EndAfterUpdateEndsChild()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            repeater.End();

            Assert.AreEqual(true, childNode.HasEnded);
        }

        [TestMethod]
        public void EndOnChildFailure()
        {
            Assert.Fail("NO TEST");
        }

        [TestMethod]
        public void EndOnChildSuccess()
        {
            Assert.Fail("NO TEST");
        }

        [TestMethod]
        public void EndOnChildError()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Error);
            repeater.Update();

            Assert.AreEqual(NodeState.Error, repeater.Status);
            Assert.AreEqual(true, childNode.HasEnded);
        }

        [TestMethod]
        public void RepeatWithChildSuccess()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new AdjustableResultNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Successful);
            repeater.Update();

            Assert.AreEqual(NodeState.Successful, repeater.Status);
        }

        [TestMethod]
        public void RepeatWithChildFailure()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new AdjustableResultNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            childNode.SetStatusOnNextUpdate(NodeState.Failure);
            repeater.Update();

            Assert.AreEqual(NodeState.Failure, repeater.Status);
        }

        [TestMethod]
        public void RepeatWithChildError()
        {
            Assert.Fail("NO TEST");
        }

        [TestMethod]
        public void RepeatWithChildActive()
        {
            var repeater = new RepeaterInfinite();
            var childNode = new AdjustableResultNode(NodeState.Inactive);
            repeater.Child = childNode;
            var status = NodeState.Error;
            var count = 1;

            repeater.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Active);

            for (; count < 1000; count++)
            {
                repeater.Update();
                status = repeater.Status;
                if (status != NodeState.Active) break;
            }

            Assert.AreEqual(1000, count);
        }

    }
}
