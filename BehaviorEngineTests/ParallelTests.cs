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

            parallel.StartRoutine();
            parallel.Update();

            Assert.AreEqual(NodeState.Error, parallel.Status);
        }

        [TestMethod]
        public void OneChild()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(childNode);

            parallel.StartRoutine();
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

            parallel.StartRoutine();
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

            parallel.StartRoutine();
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

            parallel.EndNode();

            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void InactiveChildNotEndedAgain()
        {
            var parallel = new Parallel();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            parallel.Children.Add(childNode);

            parallel.EndNode();

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

            parallel.StartRoutine();
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

            parallel.StartRoutine();
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

            parallel.StartRoutine();
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

            parallel.StartRoutine();
            parallel.Update();

            Assert.AreEqual(NodeState.Successful, parallel.Status);
        }

        [TestMethod]
        public void ChildrenStopUpdatingWhenComplete()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);
            parallel.ShouldUpdateUntilAllChildrenComplete = true;

            parallel.StartRoutine();
            parallel.Update();
            parallel.Update();
            firstChildNode.SetStatusOnNextUpdate(NodeState.Successful);
            parallel.Update();
            parallel.Update();
            secondChildNode.SetStatusOnNextUpdate(NodeState.Successful);
            parallel.Update();

            Assert.AreEqual(3, firstChildNode.UpdatesTotal);
            Assert.AreEqual(5, secondChildNode.UpdatesTotal);
        }

        [TestMethod]
        public void SuccessWithAllChildren()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);
            parallel.ShouldUpdateUntilAllChildrenComplete = true;

            parallel.StartRoutine();
            parallel.Update();
            firstChildNode.SetStatusOnNextUpdate(NodeState.Successful);
            parallel.Update();
            secondChildNode.SetStatusOnNextUpdate(NodeState.Successful);
            parallel.Update();

            Assert.AreEqual(2, firstChildNode.UpdatesTotal);
            Assert.AreEqual(3, secondChildNode.UpdatesTotal);
            Assert.AreEqual(NodeState.Successful, parallel.Status);
        }

        [TestMethod]
        public void FailureWithAllChildren()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);
            parallel.ShouldUpdateUntilAllChildrenComplete = true;

            parallel.StartRoutine();
            parallel.Update();
            firstChildNode.SetStatusOnNextUpdate(NodeState.Failure);
            parallel.Update();
            secondChildNode.SetStatusOnNextUpdate(NodeState.Failure);
            parallel.Update();

            Assert.AreEqual(2, firstChildNode.UpdatesTotal);
            Assert.AreEqual(3, secondChildNode.UpdatesTotal);
            Assert.AreEqual(NodeState.Failure, parallel.Status);
        }

        [TestMethod]
        public void ErrorInterruptsUpdateWithAllChildren()
        {
            var parallel = new Parallel();
            var firstChildNode = new EventTrackingNode(NodeState.Active);
            var secondChildNode = new EventTrackingNode(NodeState.Active);
            parallel.Children.Add(firstChildNode);
            parallel.Children.Add(secondChildNode);
            parallel.ShouldUpdateUntilAllChildrenComplete = true;

            parallel.StartRoutine();
            parallel.Update();
            firstChildNode.SetStatusOnNextUpdate(NodeState.Error);
            parallel.Update();

            Assert.AreEqual(NodeState.Error, parallel.Status);
        }
    }
}
