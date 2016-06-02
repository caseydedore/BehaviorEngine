using BehaviorEngine;
using BehaviorEngineTests.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEngineTests
{
    [TestClass]
    public class OverriderTests
    {
        [TestMethod]
        public void NoChild()
        {
            var overrider = new Overrider();

            overrider.Start();
            overrider.Update();

            Assert.AreEqual(NodeState.Error, overrider.Status);
        }

        [TestMethod]
        public void StartChild()
        {
            var overrider = new Overrider();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            overrider.Child = childNode;

            overrider.Start();
            overrider.Update();

            Assert.IsTrue(childNode.HasStarted);
        }

        [TestMethod]
        public void EndChild()
        {
            var overrider = new Overrider();
            var childNode = new EventTrackingNode(NodeState.Inactive);
            overrider.Child = childNode;

            overrider.Start();
            overrider.Update();
            overrider.End();

            Assert.IsTrue(childNode.HasEnded);
        }

        [TestMethod]
        public void OverrideChildSuccess()
        {
            var overrider = new Overrider();
            var overrideStatus = NodeState.Failure;
            overrider.SuccessOverride = overrideStatus;
            var childNode = new EventTrackingNode(NodeState.Inactive);
            overrider.Child = childNode;

            overrider.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Successful);
            overrider.Update();

            Assert.AreEqual(overrideStatus, overrider.Status);
        }

        [TestMethod]
        public void OverrideChildActive()
        {
            var overrider = new Overrider();
            var overrideStatus = NodeState.Active;
            overrider.ActiveOverride = overrideStatus;
            var childNode = new EventTrackingNode(NodeState.Inactive);
            overrider.Child = childNode;

            overrider.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Active);
            overrider.Update();

            Assert.AreEqual(overrideStatus, overrider.Status);
        }

        [TestMethod]
        public void OverrideChildFailure()
        {
            var overrider = new Overrider();
            var overrideStatus = NodeState.Successful;
            overrider.FailureOverride = overrideStatus;
            var childNode = new EventTrackingNode(NodeState.Inactive);
            overrider.Child = childNode;

            overrider.Start();
            childNode.SetStatusOnNextUpdate(NodeState.Failure);
            overrider.Update();

            Assert.AreEqual(overrideStatus, overrider.Status);
        }
    }
}
