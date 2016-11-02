//using BehaviorEngine;
//using BehaviorEngineTests.Nodes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;

//namespace BehaviorEngineTests
//{
//    [TestClass]
//    public class SelectorTests
//    {
//        [TestMethod]
//        public void ChildSuccess()
//        {
//            var selector = new Selector();
//            var childNode = new EventTrackingNode(NodeState.Successful);
//            selector.Children.Add(childNode);

//            selector.StartRoutine();
//            selector.Update();

//            Assert.IsTrue(childNode.HasStarted);
//            Assert.AreEqual(1, childNode.UpdatesTotal);
//            Assert.AreEqual(NodeState.Successful, selector.Status);
//        }

//        [TestMethod]
//        public void ChildFails()
//        {
//            var selector = new Selector();
//            var childNode = new EventTrackingNode(NodeState.Failure);
//            selector.Children.Add(childNode);

//            selector.StartRoutine();
//            selector.Update();

//            Assert.IsTrue(childNode.HasStarted);
//            Assert.AreEqual(1, childNode.UpdatesTotal);
//            Assert.AreEqual(NodeState.Failure, selector.Status);
//        }

//        [TestMethod]
//        public void ChildActive()
//        {
//            var selector = new Selector();
//            var childNode = new EventTrackingNode(NodeState.Active);
//            selector.Children.Add(childNode);

//            selector.StartRoutine();
//            selector.Update();

//            Assert.IsTrue(childNode.HasStarted);
//            Assert.IsFalse(childNode.HasEnded);
//            Assert.AreEqual(1, childNode.UpdatesTotal);
//            Assert.AreEqual(NodeState.Active, selector.Status);
//        }

//        [TestMethod]
//        public void ChildError()
//        {
//            var selector = new Selector();
//            var childNode = new EventTrackingNode(NodeState.Error);
//            selector.Children.Add(childNode);

//            selector.StartRoutine();
//            selector.Update();

//            Assert.IsTrue(childNode.HasStarted);
//            Assert.AreEqual(1, childNode.UpdatesTotal);
//            Assert.AreEqual(NodeState.Error, selector.Status);
//        }

//        [TestMethod]
//        public void FirstChildSucceeds()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Successful);
//            var secondChild = new EventTrackingNode(NodeState.Successful);
//            selector.Children.AddRange( new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();

//            Assert.AreEqual(1, firstChild.UpdatesTotal);
//            Assert.AreEqual(0, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Successful, selector.Status);
//        }

//        [TestMethod]
//        public void FirstChildTakesPrecedenceOverSecond()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Failure);
//            var secondChild = new EventTrackingNode(NodeState.Active);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();
//            firstChild.SetStatusOnNextUpdate(NodeState.Active);
//            selector.Update();
//            selector.Update();

//            Assert.AreEqual(3, firstChild.UpdatesTotal);
//            Assert.AreEqual(1, secondChild.UpdatesTotal);
//        }

//        [TestMethod]
//        public void FirstChildFails()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Failure);
//            var secondChild = new EventTrackingNode(NodeState.Successful);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();

//            Assert.AreEqual(1, firstChild.UpdatesTotal);
//            Assert.AreEqual(1, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Successful, selector.Status);
//        }

//        [TestMethod]
//        public void SecondChildSucceeds()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Failure);
//            var secondChild = new EventTrackingNode(NodeState.Successful);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();

//            Assert.AreEqual(1, firstChild.UpdatesTotal);
//            Assert.AreEqual(1, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Successful, selector.Status);
//        }

//        [TestMethod]
//        public void AllChildrenFail()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Failure);
//            var secondChild = new EventTrackingNode(NodeState.Failure);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();

//            Assert.AreEqual(1, firstChild.UpdatesTotal);
//            Assert.AreEqual(1, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Failure, selector.Status);
//        }

//        [TestMethod]
//        public void FirstChildActive()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Active);
//            var secondChild = new EventTrackingNode(NodeState.Successful);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();
//            selector.Update();
//            selector.Update();

//            Assert.AreEqual(3, firstChild.UpdatesTotal);
//            Assert.AreEqual(0, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Active, selector.Status);
//        }

//        [TestMethod]
//        public void SecondChildActive()
//        {
//            var selector = new Selector();
//            var firstChild = new EventTrackingNode(NodeState.Failure);
//            var secondChild = new EventTrackingNode(NodeState.Active);
//            selector.Children.AddRange(new List<INode>() { firstChild, secondChild });

//            selector.StartRoutine();
//            selector.Update();
//            selector.Update();
//            selector.Update();

//            Assert.AreEqual(3, firstChild.UpdatesTotal);
//            Assert.AreEqual(3, secondChild.UpdatesTotal);
//            Assert.AreEqual(NodeState.Active, selector.Status);
//        }
//    }
//}
