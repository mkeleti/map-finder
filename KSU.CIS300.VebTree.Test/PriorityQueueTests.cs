// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KSU.CIS300.VebTree.Test
{
    [TestFixture]
    public class PriorityQueueTests
    {
        [Test]
        [Category("L-PriorityQueue")]
        public void Basic()
        {
            MinPriorityQueue q = new MinPriorityQueue(16);
            q.Enqueue(2);
            q.Enqueue(1);
            q.Enqueue(5);
            q.Enqueue(3);
            q.Enqueue(14);
            Assert.AreEqual(5, q.Count);
            Assert.AreEqual(1, q.Dequeue());
            Assert.AreEqual(4, q.Count);

            Assert.AreEqual(2, q.Dequeue());
            Assert.AreEqual(3, q.Dequeue());
            Assert.AreEqual(5, q.Dequeue());
            Assert.AreEqual(14, q.Dequeue());

            Assert.AreEqual(0, q.Count);
        }

        [Test]
        [Category("L-PriorityQueue")]
        public void DequeueEmpty()
        {
            MinPriorityQueue q = new MinPriorityQueue(16);
            q.Enqueue(2);
            Assert.AreEqual(2, q.Dequeue());
            Assert.AreEqual(0, q.Count);
            Assert.Throws<InvalidOperationException>(() => q.Dequeue());
        }

        [Test, Timeout(10000)]
        [Category("Z-Torture")]
        public void PriorityQueueHuge()
        {
            int max = 1048576;
            MinPriorityQueue q = new MinPriorityQueue(max);

            foreach (int k in Enumerable.Range(0, max))
            {
                q.Enqueue(k);
            }

            Assert.AreEqual(max, q.Count);

            foreach (int k in Enumerable.Range(0, max))
            {
                Assert.AreEqual(k, q.Dequeue());
            }

            Assert.AreEqual(0, q.Count);
        }
    
        [Test, Timeout(5000)]
        [Category("Z-Torture")]
        public void PriorityQueueLarge()
        {
            int max = 524288;
            MinPriorityQueue q = new MinPriorityQueue(max);

            foreach (int k in Enumerable.Range(0, max))
            {
                q.Enqueue(k);
            }

            Assert.AreEqual(max, q.Count);

            foreach (int k in Enumerable.Range(0, max))
            {
                Assert.AreEqual(k, q.Dequeue()); 
            }

            Assert.AreEqual(0, q.Count);
        }
    }
}
