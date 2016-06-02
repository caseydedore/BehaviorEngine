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
            var nodeStatus = NodeState.Inactive;

            //act
            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            //assert
            Assert.AreEqual(NodeState.Error, nodeStatus);
        }

        [TestMethod]
        public void ChildSuccessful()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Successful);
            var nodeStatus = NodeState.Inactive;

            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            Assert.AreEqual(NodeState.Successful, nodeStatus);
        }

        [TestMethod]
        public void ChildFailure()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Failure);
            var nodeStatus = NodeState.Inactive;

            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            Assert.AreEqual(NodeState.Failure, nodeStatus);
        }

        [TestMethod]
        public void ChildActive()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Active);
            var nodeStatus = NodeState.Inactive;

            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            Assert.AreEqual(NodeState.Active, nodeStatus);
        }

        [TestMethod]
        public void ChildError()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Error);
            var nodeStatus = NodeState.Inactive;

            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            Assert.AreEqual(NodeState.Error, nodeStatus);
        }

        [TestMethod]
        public void IgnoreChildSuccess()
        {
            var repeater = new Repeater(2, true);
            repeater.Child = new FixedResultNode(NodeState.Successful);

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(NodeState.Active, repeater.Status);
        }

        [TestMethod]
        public void IgnoreChildFailure()
        {
            var repeater = new Repeater(2, true);
            repeater.Child = new FixedResultNode(NodeState.Failure);

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(NodeState.Active, repeater.Status);
        }

        [TestMethod]
        public void IgnoreChildError()
        {
            var repeater = new Repeater(2, true);
            repeater.Child = new FixedResultNode(NodeState.Error);

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(NodeState.Active, repeater.Status);
        }

        [TestMethod]
        public void RepeatMultipleTimesIgnoringChildTrackEvents()
        {
            var repeater = new Repeater(5, true);
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            repeater.Update();
            repeater.Update();
            repeater.Update();

            Assert.AreEqual(4, childNode.StartsTotal, "child start events");
            Assert.AreEqual(4, childNode.UpdatesTotal, "child update events");
            Assert.AreEqual(4, childNode.EndsTotal, "child end events");
        }

        [TestMethod]
        public void RepeatOneTimeTrackEvents()
        {
            var repeater = new Repeater(1);
            var childNode = new EventTrackingNode(NodeState.Inactive);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(1, childNode.StartsTotal);
            Assert.AreEqual(1, childNode.UpdatesTotal);
            Assert.AreEqual(1, childNode.EndsTotal);
        }

        [TestMethod]
        public void ChildEndsWhenRepeaterEnds()
        {
            var repeater = new Repeater(3, true);
            var childNode = new EventTrackingNode(NodeState.Successful);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            repeater.Update();
            repeater.Update();

            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void ChildStartsWhenRepeaterUpdatesFirstTime()
        {
            var repeater = new Repeater(5);
            var childNode = new EventTrackingNode(NodeState.Error);
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(1, childNode.StartsTotal);
        }

        [TestMethod]
        public void RepeatInactiveWithNoCount()
        {
            var repeater = new Repeater(0);
            repeater.Child = new FixedResultNode(NodeState.Active);

            repeater.Start();
            repeater.Update();

            Assert.AreEqual(NodeState.Inactive, repeater.Status);
        }

        [TestMethod]
        public void RepeatInactiveAfterCountDepletion()
        {
            var repeater = new Repeater(1);
            repeater.Child = new FixedResultNode(NodeState.Inactive);

            repeater.Start();
            repeater.Update();
            repeater.Update();

            Assert.AreEqual(NodeState.Inactive, repeater.Status);
        }
    }
}
