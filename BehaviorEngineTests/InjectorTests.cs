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
    public class InjectorTests
    {
        [TestMethod]
        public void NoChild()
        {
            var injector = new Injector(() => NodeState.Failure);

            injector.Start();
            injector.Update();

            Assert.AreEqual(NodeState.Error, injector.Status);
        }

        [TestMethod]
        public void ChildActive()
        {
            var injector = new Injector(() => NodeState.Failure);
            injector.Child = new FixedStateNode(NodeState.Active);

            injector.Start();
            injector.Update();

            Assert.AreEqual(NodeState.Active, injector.Status);
        }

        [TestMethod]
        public void ChildSuccess()
        {
            var injector = new Injector(() => NodeState.Failure);
            injector.Child = new FixedStateNode(NodeState.Successful);

            injector.Start();
            injector.Update();

            Assert.AreEqual(NodeState.Failure, injector.Status);
        }

        [TestMethod]
        public void ChildFailure()
        {
            var injector = new Injector(() => NodeState.Successful);
            injector.Child = new FixedStateNode(NodeState.Failure);

            injector.Start();
            injector.Update();

            Assert.AreEqual(NodeState.Successful, injector.Status);
        }

        [TestMethod]
        public void ChildError()
        {
            var injector = new Injector(() => NodeState.Successful);
            injector.Child = new FixedStateNode(NodeState.Error);

            injector.Start();
            injector.Update();

            Assert.AreEqual(NodeState.Error, injector.Status);
        }
    }
}
