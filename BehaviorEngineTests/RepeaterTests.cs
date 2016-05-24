using BehaviorEngine;
using BehaviorEngineTests.TestNodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorEngineTests
{
    [TestClass]
    public class RepeaterTests
    {
        [TestMethod]
        public void NoChildTest()
        {
            //arrange
            var repeater = new Repeater();
            var nodeStatus = NodeState.Error;

            //act
            nodeStatus = repeater.Update();

            //assert
            Assert.AreEqual(nodeStatus, NodeState.Error);
        }

        [TestMethod]
        public void SuccessChildTest()
        {
            //arrange
            var repeater = new Repeater();
            repeater.Child = new FixedResultNode(NodeState.Successful);
            var nodeStatus = NodeState.Error;

            //act
            nodeStatus = repeater.Update();

            //assert
            Assert.AreEqual(nodeStatus, NodeState.Successful);
        }

        [TestMethod]
        public void FailureChildTest()
        {
            //arrange
            var repeater = new Repeater();
            repeater.Child = new FixedResultNode(NodeState.Failure);
            var nodeStatus = NodeState.Error;

            //act
            nodeStatus = repeater.Update();

            //assert
            Assert.AreEqual(nodeStatus, NodeState.Failure);
        }

        [TestMethod]
        public void ActiveChildTest()
        {
            //arrange
            var repeater = new Repeater();
            repeater.Child = new FixedResultNode(NodeState.Active);
            var nodeStatus = NodeState.Error;

            //act
            nodeStatus = repeater.Update();

            //assert
            Assert.AreEqual(nodeStatus, NodeState.Active);
        }

        [TestMethod]
        public void RepeatForeverTest()
        {
            var repeater = new Repeater();
            repeater.Child = new FixedResultNode(NodeState.Successful);
            var status = NodeState.Error;
            var count = 0;
            var expectedCount = 10000;

            for(; count < expectedCount; count++)
            {
                status = repeater.Update();

                if (status != NodeState.Active) break;
            }

            Assert.AreEqual(count, expectedCount);
        }

        [TestMethod]
        public void RepeatForeverUntilChildNotActive()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RepeatOneTimeTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RepeatFiveTimesTest()
        {
            Assert.Fail();
        }
    }
}
