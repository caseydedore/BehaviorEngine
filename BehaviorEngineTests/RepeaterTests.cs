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
            var nodeStatus = NodeState.Error;

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
            var nodeStatus = NodeState.Error;

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
            var nodeStatus = NodeState.Error;

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
            var nodeStatus = NodeState.Error;

            repeater.Start();
            repeater.Update();
            nodeStatus = repeater.Status;

            Assert.AreEqual(NodeState.Error, nodeStatus);
        }

        [TestMethod]
        public void RepeatOneTimeIgnoringChild()
        {
            var repeater = new Repeater(1, true);
            repeater.Child = new FixedResultNode(NodeState.Active);
            var status = NodeState.Error;

            repeater.Start();
            repeater.Update();
            status = repeater.Status;

            Assert.AreEqual(NodeState.Successful, status);
        }

        [TestMethod]
        public void RepeatMultipleTimesIgnoringChild()
        {
            uint timesToRepeat = 5;
            var repeater = new Repeater(timesToRepeat, true);
            repeater.Child = new FixedResultNode(NodeState.Failure);
            var status = NodeState.Error;
            uint count = 1;

            repeater.Start();

            for (; count < 100; count++)
            {
                repeater.Update();
                status = repeater.Status;

                if (status == NodeState.Successful) break;
            }

            Assert.AreEqual(timesToRepeat, count);
        }

        [TestMethod]
        public void RepeatMultipleTimesWithChildActive()
        {
            RepeatMultipleTimesStopOnChild(NodeState.Error, NodeState.Active, 51);
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildFailure()
        {
            RepeatMultipleTimesStopOnChild(NodeState.Error, NodeState.Failure, 51);
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildSuccessful()
        {
            RepeatMultipleTimesStopOnChild(NodeState.Error, NodeState.Successful, 51);
        }

        [TestMethod]
        public void RepeatMultipleTimesStopOnChildError()
        {
            RepeatMultipleTimesStopOnChild(NodeState.Successful, NodeState.Error, 51);
        }

        private void RepeatMultipleTimesStopOnChild(NodeState childReturn, NodeState childReturnFinal, int countUntilChildStops)
        {
            var repeater = new Repeater((uint)(countUntilChildStops + 10));
            var childNode = new AdjustableResultNode() { Status = childReturn };
            repeater.Child = childNode;
            var status = NodeState.Error;
            var count = 1;


            for (; count < (countUntilChildStops + 10); count++)
            {
                if (count == countUntilChildStops) childNode.Status = childReturnFinal;

                repeater.Update();
                status = repeater.Status;

                if (status == childReturnFinal) break;
            }

            Assert.AreEqual(countUntilChildStops, count);
        }

        [TestMethod]
        public void ChildEndsWhenRepeaterEnds()
        {
            var repeater = new Repeater(3, true);
            var childNode = new EventTrackingNode() { Status = NodeState.Successful };
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            repeater.Update();
            repeater.Update();
            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void ChildEndsWhenRepeaterEndsDueToChildSuccess()
        {
            ChildEndsWhenRepeaterEndsDueToChild(NodeState.Successful);
        }

        [TestMethod]
        public void ChildEndsWhenRepeaterEndsDueToChildFailure()
        {
            ChildEndsWhenRepeaterEndsDueToChild(NodeState.Failure);
        }

        [TestMethod]
        public void ChildEndsWhenRepeaterEndsDueToChildError()
        {
            ChildEndsWhenRepeaterEndsDueToChild(NodeState.Error);
        }

        private void ChildEndsWhenRepeaterEndsDueToChild(NodeState childStateEnd)
        {
            var repeater = new Repeater(5, true);
            var childNode = new EventTrackingNode() { Status = NodeState.Active };
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            repeater.Update();
            childNode.Status = childStateEnd;
            repeater.Update();
            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void ChildStartsWhenRepeaterUpdatesFirstTime()
        {
            var repeater = new Repeater(5);
            var childNode = new EventTrackingNode() { Status = NodeState.Error };
            repeater.Child = childNode;

            repeater.Start();
            repeater.Update();
            Assert.IsTrue(childNode.HasStarted);
        }
    }
}
