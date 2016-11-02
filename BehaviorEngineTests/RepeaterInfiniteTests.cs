//using BehaviorEngine;
//using BehaviorEngineTests.Nodes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace BehaviorEngineTests
//{
//    [TestClass]
//    public class RepeaterInfiniteTests
//    {
//        [TestMethod]
//        public void ChildMissing()
//        {
//            //arrange
//            var repeater = new RepeaterInfinite();
//            var nodeStatus = NodeState.Inactive;

//            //act
//            repeater.StartRoutine();
//            repeater.Update();
//            nodeStatus = repeater.Status;

//            //assert
//            Assert.AreEqual(NodeState.Error, nodeStatus);
//        }

//        [TestMethod]
//        public void StartChild()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();

//            Assert.AreEqual(true, childNode.HasStarted);
//        }

//        [TestMethod]
//        public void EndBeforeUpdate()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.EndNode();

//            Assert.AreEqual(false, childNode.HasStarted);
//            Assert.AreEqual(false, childNode.HasEnded);
//            Assert.AreEqual(0, childNode.UpdatesTotal);
//        }

//        [TestMethod]
//        public void EndChild()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            repeater.EndNode();

//            Assert.AreEqual(true, childNode.HasEnded);
//        }

//        [TestMethod]
//        public void ChildFailure()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            repeater.Update();
//            repeater.Update();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Failure);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Failure, repeater.Status);
//            Assert.AreEqual(5, childNode.UpdatesTotal);
//            Assert.AreEqual(true, childNode.HasEnded);
//        }

//        [TestMethod]
//        public void ChildSuccess()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Successful);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Successful, repeater.Status);
//            Assert.AreEqual(3, childNode.UpdatesTotal);
//            Assert.AreEqual(true, childNode.HasEnded);
//        }

//        [TestMethod]
//        public void ChildError()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            childNode.SetStatusOnNextUpdate(NodeState.Error);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Error, repeater.Status);
//            Assert.AreEqual(true, childNode.HasEnded);
//        }

//        [TestMethod]
//        public void EndOnChildFailure()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Failure);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Failure, repeater.Status);
//        }

//        [TestMethod]
//        public void EndOnChildError()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Error);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Error, repeater.Status);
//        }

//        [TestMethod]
//        public void ChildActive()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;
//            var status = NodeState.Error;
//            var count = 1;

//            repeater.StartRoutine();
//            childNode.SetStatusOnNextUpdate(NodeState.Active);

//            for (; count < 1000; count++)
//            {
//                repeater.Update();
//                status = repeater.Status;
//                if (status != NodeState.Active) break;
//            }

//            Assert.AreEqual(1000, count);
//        }

//        [TestMethod]
//        public void IgnoreChildFailure()
//        {
//            var repeater = new RepeaterInfinite(true);
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Failure);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Active, repeater.Status);
//        }

//        [TestMethod]
//        public void IgnoreChildError()
//        {
//            var repeater = new RepeaterInfinite(true);
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Error);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Active, repeater.Status);
//        }

//        [TestMethod]
//        public void IgnoreChildSuccess()
//        {
//            var repeater = new RepeaterInfinite(true);
//            var childNode = new AdjustableStateNode(NodeState.Inactive);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            childNode.SetStatusOnNextUpdate(NodeState.Successful);
//            repeater.Update();

//            Assert.AreEqual(NodeState.Active, repeater.Status);
//        }

//        [TestMethod]
//        public void ChildEndsWhenRepeaterEnds()
//        {
//            var repeater = new RepeaterInfinite(true);
//            var childNode = new EventTrackingNode(NodeState.Successful);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();
//            repeater.Update();
//            repeater.Update();
//            repeater.EndNode();

//            Assert.IsTrue(childNode.HasEnded);
//        }

//        [TestMethod]
//        public void ChildStartsWhenRepeaterUpdatesFirstTime()
//        {
//            var repeater = new RepeaterInfinite();
//            var childNode = new EventTrackingNode(NodeState.Error);
//            repeater.Child = childNode;

//            repeater.StartRoutine();
//            repeater.Update();

//            Assert.IsTrue(childNode.HasStarted);
//            Assert.AreEqual(1, childNode.StartsTotal);
//        }
//    }
//}
