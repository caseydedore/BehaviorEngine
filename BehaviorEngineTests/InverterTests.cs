//using BehaviorEngine;
//using BehaviorEngineTests.Nodes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace BehaviorEngineTests
//{
//    [TestClass]
//    public class InverterTests
//    {
//        [TestMethod]
//        public void NoChild()
//        {
//            var inverter = new Inverter();

//            inverter.Start();
//            inverter.Update();

//            Assert.AreEqual(NodeState.Error, inverter.Status);
//        }

//        [TestMethod]
//        public void ChildSuccess()
//        {
//            var inverter = new Inverter();
//            var childNode = new FixedStateNode(NodeState.Successful);
//            inverter.Child = childNode;

//            inverter.Start();
//            inverter.Update();

//            Assert.AreEqual(NodeState.Failure, inverter.Status);
//        }

//        [TestMethod]
//        public void ChildFailure()
//        {
//            var inverter = new Inverter();
//            var childNode = new FixedStateNode(NodeState.Failure);
//            inverter.Child = childNode;

//            inverter.Start();
//            inverter.Update();

//            Assert.AreEqual(NodeState.Successful, inverter.Status);
//        }

//        [TestMethod]
//        public void ChildError()
//        {
//            var inverter = new Inverter();
//            var childNode = new FixedStateNode(NodeState.Error);
//            inverter.Child = childNode;

//            inverter.Start();
//            inverter.Update();

//            Assert.AreEqual(NodeState.Error, inverter.Status);
//        }

//        [TestMethod]
//        public void ChildActive()
//        {
//            var inverter = new Inverter();
//            var childNode = new FixedStateNode(NodeState.Active);
//            inverter.Child = childNode;

//            inverter.Start();
//            inverter.Update();

//            Assert.AreEqual(NodeState.Active, inverter.Status);
//        }
//    }
//}
