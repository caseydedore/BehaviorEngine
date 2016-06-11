using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BehaviorEngineTests
{
    [TestClass]
    public class ParallelTests
    {
        [TestMethod]
        public void NoChildren()
        {
            var parallel = new Parallel();

            parallel.Start();
            parallel.Update();

            Assert.AreEqual(NodeState.Error, parallel.Status);
        }

        [TestMethod]
        public void OneChild()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(childNode);

            parallel.Start();
            parallel.Update();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(NodeState.Active, parallel.Status);
        }

        [TestMethod]
        public void TwoChildren()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();
            parallel.Update();
            parallel.Update();

            Assert.IsTrue(firstChildNode.HasStarted);
            Assert.AreEqual(3, firstChildNode.UpdatesTotal);
            Assert.IsTrue(secondChildNode.HasStarted);
            Assert.AreEqual(3, secondChildNode.UpdatesTotal);
            Assert.AreEqual(NodeState.Active, parallel.Status);
        }

        [TestMethod]
        public void FirstChildFailsWithFailurePolicyAll()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Failure);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();
            parallel.Update();

            Assert.IsTrue(firstChildNode.HasStarted);
            Assert.AreEqual(3, firstChildNode.UpdatesTotal);
            Assert.IsTrue(secondChildNode.HasStarted);
            Assert.AreEqual(3, secondChildNode.UpdatesTotal);
            Assert.AreEqual(NodeState.Active, parallel.Status);
        }
    }
}
