using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BehaviorEngineTests
{
    [TestClass]
    public class RepeaterTests
    {
        [TestMethod]
        public void ChildMissing()
        {
            //arrange
            var repeater = new Repeater(5);
            var nodeStatus = NodeState.Error;

            //act
            repeater.Start();
            nodeStatus = repeater.Update();

            //assert
            Assert.AreEqual(NodeState.Error, nodeStatus);
        }

        [TestMethod]
        public void ChildSuccessful()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Successful);
            var nodeStatus = NodeState.Error;

            repeater.Start();
            nodeStatus = repeater.Update();

            Assert.AreEqual(NodeState.Successful, nodeStatus);
        }

        [TestMethod]
        public void ChildFailure()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Failure);
            var nodeStatus = NodeState.Error;

            repeater.Start();
            nodeStatus = repeater.Update();

            Assert.AreEqual(NodeState.Failure, nodeStatus);
        }

        [TestMethod]
        public void ChildActive()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Active);
            var nodeStatus = NodeState.Error;

            repeater.Start();
            nodeStatus = repeater.Update();

            Assert.AreEqual(NodeState.Active, nodeStatus);
        }

        [TestMethod]
        public void ChildError()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Error);
            var nodeStatus = NodeState.Error;

            repeater.Start();
            nodeStatus = repeater.Update();

            Assert.AreEqual(NodeState.Error, nodeStatus);
        }

        [TestMethod]
        public void RepeatOneTimeIgnoringChild()
        {
            var repeater = new Repeater(1, true);
            repeater.Child = new FixedResultNode(NodeState.Active);
            var status = NodeState.Error;

            repeater.Start();
            status = repeater.Update();

            Assert.AreEqual(NodeState.Successful, status);
        }

        [TestMethod]
        public void RepeatFiveTimesIgnoringChild()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildFailure()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildSuccessful()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildError()
        {
            Assert.Fail();
        }
    }
}
