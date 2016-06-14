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

            Assert.AreEqual(NodeState.Active, parallel.Status);
        }

        [TestMethod]
        public void TwoChildren()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Inactive);
            var secondChildNode = new EventTrackingNode(NodeState.Inactive);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            firstChildNode.SetStatusOnNextUpdate(NodeState.Active);
            secondChildNode.SetStatusOnNextUpdate(NodeState.Active);
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
        public void InactiveChildGetsStarted()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            parallel.Children.Add(childNode);

            parallel.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Active);
            parallel.Update();

            Assert.IsTrue(childNode.HasStarted);
            Assert.AreEqual(NodeState.Active, parallel.Status);
        }

        [TestMethod]
        public void ActiveChildGetsEnded()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(childNode);

            parallel.End();

            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void InactiveChildNotEndedAgain()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            parallel.Children.Add(childNode);

            parallel.End();

            Assert.IsFalse(childNode.HasEnded);
        }

        [TestMethod]
        public void EndOnFirstChildFail()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Failure);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();

            Assert.AreEqual(NodeState.Failure, parallel.Status);
        }

        [TestMethod]
        public void EndOnSecondChildFail()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Failure);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();

            Assert.AreEqual(NodeState.Failure, parallel.Status);
        }

        [TestMethod]
        public void EndOnFirstChildSuccess()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Successful);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();


            Assert.AreEqual(NodeState.Successful, parallel.Status);
        }

        [TestMethod]
        public void EndOnSecondChildSuccess()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Successful);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);

            parallel.Start();
            parallel.Update();

            Assert.AreEqual(NodeState.Successful, parallel.Status);
        }

        [TestMethod]
        public void AllMustFailButOneChildFails()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AllMustFailAndAllChildrenFail()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AllMustSucceedButOneChildSucceeds()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AllMustSucceedAndAllChildrenSucceed()
        {
            Assert.Fail();
        }
    }
}
