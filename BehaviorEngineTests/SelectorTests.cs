using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BehaviorEngineTests
{
    [TestClass]
    public class SelectorTests
    {
        [TestMethod]
        public void NoChildren()
        {
            var selector = new Selector();

            selector.Start();
            selector.Update();

            Assert.AreEqual(NodeState.Error, selector.Status);
        }

        [TestMethod]
        public void ChildSuccess()
        {
            var selector = new Selector();
            var childNode = new EventTrackingNode(NodeState.Successful);
            selector.Children.Add(childNode);

            selector.Start();
            selector.Update();
            selector.End();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(1, childNode.UpdatesTotal);
            Assert.IsTrue(childNode.HasEnded);
            Assert.AreEqual(NodeState.Successful, selector.Status);
        }

        [TestMethod]
        public void ChildFails()
        {
            var selector = new Selector();
            var childNode = new EventTrackingNode(NodeState.Failure);
            selector.Children.Add(childNode);

            selector.Start();
            selector.Update();
            selector.End();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(1, childNode.UpdatesTotal);
            Assert.IsTrue(childNode.HasEnded);
            Assert.AreEqual(NodeState.Failure, selector.Status);
        }

        [TestMethod]
        public void ChildActive()
        {
            var selector = new Selector();
            var childNode = new EventTrackingNode(NodeState.Active);
            selector.Children.Add(childNode);

            selector.Start();
            selector.Update();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(1, childNode.UpdatesTotal);
            Assert.IsFalse(childNode.HasEnded);
            Assert.AreEqual(NodeState.Active, selector.Status);
        }

        [TestMethod]
        public void ChildError()
        {
            var selector = new Selector();
            var childNode = new EventTrackingNode(NodeState.Error);
            selector.Children.Add(childNode);

            selector.Start();
            selector.Update();
            selector.End();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(1, childNode.UpdatesTotal);
            Assert.IsTrue(childNode.HasEnded);
            Assert.AreEqual(NodeState.Error, selector.Status);
        }

        [TestMethod]
        public void FirstChildSucceeds()
        {
            var selector = new Selector();
            var firstChild = new EventTrackingNode(NodeState.Successful);
            var secondChild = new EventTrackingNode(NodeState.Successful);
            selector.Children.AddRange( new List<ANode>() { firstChild, secondChild });

            selector.Start();
            selector.Update();
            selector.End();

            Assert.AreEqual(1, firstChild.UpdatesTotal);
            Assert.AreEqual(0, secondChild.UpdatesTotal);
            Assert.AreEqual(NodeState.Successful, selector.Status);
        }

        [TestMethod]
        public void SecondChildSucceeds()
        {
            var selector = new Selector();
            var firstChild = new EventTrackingNode(NodeState.Failure);
            var secondChild = new EventTrackingNode(NodeState.Successful);
            selector.Children.AddRange(new List<ANode>() { firstChild, secondChild });

            selector.Start();
            selector.Update();
            selector.End();

            Assert.AreEqual(1, firstChild.UpdatesTotal);
            Assert.AreEqual(1, secondChild.UpdatesTotal);
            Assert.AreEqual(NodeState.Successful, selector.Status);
        }

        [TestMethod]
        public void AllChildrenFail()
        {
            var selector = new Selector();
            var firstChild = new EventTrackingNode(NodeState.Failure);
            var secondChild = new EventTrackingNode(NodeState.Failure);
            selector.Children.AddRange(new List<ANode>() { firstChild, secondChild });

            selector.Start();
            selector.Update();
            selector.End();

            Assert.AreEqual(1, firstChild.UpdatesTotal);
            Assert.AreEqual(1, secondChild.UpdatesTotal);
            Assert.AreEqual(NodeState.Failure, selector.Status);
        }
    }
}
