using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BehaviorEngine.Utilities;

namespace BehaviorEngineTests
{
    [TestClass]
    public class SequenceBuilderTests
    {
        private SequenceBuilder SequenceBuilder = new SequenceBuilder();


        [TestMethod]
        public void GetSequence()
        {
            int min = 7;
            int max = 10;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(sequence.Length, 4);
            Assert.AreEqual(7, sequence[0]);
            Assert.AreEqual(8, sequence[1]);
            Assert.AreEqual(9, sequence[2]);
            Assert.AreEqual(10, sequence[3]);
        }

        [TestMethod]
        public void GetSortedSequence()
        {
            int min = 1;
            int max = -2;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max, true);

            Assert.AreEqual(4, sequence.Length);
            Assert.AreEqual(-2, sequence[0]);
            Assert.AreEqual(-1, sequence[1]);
            Assert.AreEqual(0, sequence[2]);
            Assert.AreEqual(1, sequence[3]);
        }

        [TestMethod]
        public void GetSortedNegativeSequence()
        {
            int min = -5;
            int max = -7;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max, true);

            Assert.AreEqual(3, sequence.Length);
            Assert.AreEqual(-7, sequence[0]);
            Assert.AreEqual(-6, sequence[1]);
            Assert.AreEqual(-5, sequence[2]);
        }

        [TestMethod]
        public void GetZeroRangeSequence()
        {
            int min = 0;
            int max = 0;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(1, sequence.Length);
            Assert.AreEqual(0, sequence[0]);
        }

        [TestMethod]
        public void GetReverseSequence()
        {
            int min = 8;
            int max = 6;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(3, sequence.Length);
            Assert.AreEqual(8, sequence[0]);
            Assert.AreEqual(7, sequence[1]);
            Assert.AreEqual(6, sequence[2]);
        }

        [TestMethod]
        public void GetNegativeSequence()
        {
            int min = -5;
            int max = -1;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(5, sequence.Length);
            Assert.AreEqual(-5, sequence[0]);
            Assert.AreEqual(-4, sequence[1]);
            Assert.AreEqual(-3, sequence[2]);
            Assert.AreEqual(-2, sequence[3]);
            Assert.AreEqual(-1, sequence[4]);
        }

        [TestMethod]
        public void GetPositiveToNegativeSequence()
        {
            int min = 1;
            int max = -2;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(sequence.Length, 4);
            Assert.AreEqual(1, sequence[0]);
            Assert.AreEqual(0, sequence[1]);
            Assert.AreEqual(-1, sequence[2]);
            Assert.AreEqual(-2, sequence[3]);
        }

        [TestMethod]
        public void GetNegativeToPositiveSequence()
        {
            int min = -1;
            int max = 2;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetSequence(min, max);

            Assert.AreEqual(sequence.Length, 4);
            Assert.AreEqual(-1, sequence[0]);
            Assert.AreEqual(0, sequence[1]);
            Assert.AreEqual(1, sequence[2]);
            Assert.AreEqual(2, sequence[3]);
        }

        [TestMethod]
        public void GetRandomSequence()
        {
            int min = 2;
            int max = 9;
            int[] sequence = new int[] { };

            sequence = SequenceBuilder.GetRandomSequence(min, max);

            bool extendsRange = false;

            foreach(var i in sequence)
            {
                extendsRange = i < min || i > max;
            }

            Assert.AreEqual(8, sequence.Length);
            Assert.IsFalse(extendsRange);
        }
    }
}
