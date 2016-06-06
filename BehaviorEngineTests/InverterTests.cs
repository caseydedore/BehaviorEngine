using BehaviorEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BehaviorEngineTests
{
    [TestClass]
    public class InverterTests
    {
        [TestMethod]
        public void NoChild()
        {
            var inverter = new Inverter();

            inverter.Start();
            inverter.Update();

            Assert.AreEqual(NodeState.Error, inverter.Status);
        }

        [TestMethod]
        public void ChildSuccess()
        {
            Assert.Fail("No Test");
        }

        [TestMethod]
        public void ChildFailure()
        {
            Assert.Fail("No Test");

        }

        [TestMethod]
        public void ChildError()
        {
            Assert.Fail("No Test");

        }

        [TestMethod]
        public void ChildActive()
        {
            Assert.Fail("No Test");

        }
    }
}
