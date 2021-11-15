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
    public class VebTreeTests
    {
        [Test]
        [Category("A-Error Check")]
        public void VebTree_ErrorCheck()
        {
            Assert.Catch<ArgumentException>(() => new VebTreeNode(0));
            Assert.Catch<ArgumentException>(() => new VebTreeNode(3));
            Assert.Catch<ArgumentException>(() => new VebTreeNode(9));
            Assert.Catch<ArgumentException>(() => new VebTreeNode(200));
            Assert.DoesNotThrow(() => new VebTreeNode(1));
            Assert.DoesNotThrow(() => new VebTreeNode(4));
            Assert.DoesNotThrow(() => new VebTreeNode(16));
            Assert.DoesNotThrow(() => new VebTreeNode(1024));

            Assert.Catch<ArgumentException>(() => new VebTreeNode(4).Insert(4));
            Assert.Catch<ArgumentException>(() => new VebTreeNode(4).Insert(5));
            Assert.Catch<ArgumentException>(() => new VebTreeNode(4).Insert(-1));
        }

        [Test]
        [Category("B-Constructor")]
        public void VebTree_Constructor()
        {

            VebTreeNode root = new VebTreeNode(1);
            Assert.AreEqual(-1, root.Min);
            Assert.AreEqual(-1, root.Max);
            Assert.AreEqual(1, root.Universe);
            Assert.IsNull(root.Summary);
            Assert.IsNull(root.Clusters);

            root = new VebTreeNode(2);
            Assert.AreEqual(-1, root.Min);
            Assert.AreEqual(-1, root.Max);
            Assert.AreEqual(2, root.Universe);
            Assert.IsNull(root.Summary);
            Assert.IsNull(root.Clusters);

        }

        [Test]
        [Category("B-Constructor")]
        public void VebTree_ConstructorChildren()
        {

            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(-1, root.Min);
            Assert.AreEqual(-1, root.Max);
            Assert.AreEqual(8, root.Universe);
            Assert.IsNotNull(root.Summary);
            Assert.AreEqual(4, root.Summary.Universe);

            Assert.IsNotNull(root.Clusters);
            Assert.AreEqual(4, root.Clusters.Length);

            for (int i = 0; i < root.Clusters.Length; i++)
            {
                Assert.AreEqual(2, root.Clusters[i].Universe);
            }

        }


        [Test]
        [Category("C-Lower Square Root")]
        public void VebTree_LowerSqrtOdd()
        {
            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(2, root.LowerSquareRoot());
        }

        [Test]
        [Category("C-Lower Square Root")]
        public void VebTree_LowerSqrtEven()
        {
            VebTreeNode root = new VebTreeNode(1024);
            Assert.AreEqual(32, root.LowerSquareRoot());
        }

        [Test]
        [Category("D-Upper Square Root")]
        public void VebTree_UpperSqrtOdd()
        {
            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(4, root.UpperSquareRoot());
        }
       
        [Test]
        [Category("D-Upper Square Root")]
        public void VebTree_UpperSqrtEven()
        {
            VebTreeNode root = new VebTreeNode(1024);
            Assert.AreEqual(32, root.UpperSquareRoot());
        }

        [Test]
        [Category("E-Index")]
        public void VebTree_Index()
        {
            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(5, root.Index(1,3)); 
            Assert.AreEqual(4, root.Index(2,0));
            Assert.AreEqual(1, root.Index(0,1));
        }

        [Test]
        [Category("F-Low")]
        public void VebTree_Low()
        {
            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(1, root.Low(7));
            Assert.AreEqual(0, root.Low(4));
        }
        [Test]
        [Category("G-High")]
        public void VebTree_High()
        {
            VebTreeNode root = new VebTreeNode(8);
            Assert.AreEqual(3, root.High(7));
            Assert.AreEqual(2, root.High(4));
        }

        [Test]
        [Category("H-Insert")]
        public void InsertSingle()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(1);

            Assert.AreEqual(1, root.Min);
            Assert.AreEqual(1, root.Max);
            Assert.IsNotNull(root.Summary);
            Assert.IsNull(root.Summary.Clusters);
            Assert.IsNull(root.Summary.Summary);
            Assert.AreEqual(-1, root.Summary.Min);
            Assert.AreEqual(-1, root.Summary.Max);

            Assert.IsNotNull(root.Clusters);
            Assert.AreEqual(-1, root.Clusters[0].Min);
            Assert.AreEqual(-1, root.Clusters[0].Max);
            Assert.IsNull(root.Clusters[0].Clusters);
            Assert.IsNull(root.Clusters[0].Summary);

            Assert.AreEqual(-1, root.Clusters[1].Min);
            Assert.AreEqual(-1, root.Clusters[1].Max);
            Assert.IsNull(root.Clusters[1].Clusters);
            Assert.IsNull(root.Clusters[1].Summary);
        }

        [Test]
        [Category("H-Insert")]
        public void InsertTwo()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(2);


            Assert.AreEqual(4, root.Universe);
            Assert.AreEqual(0, root.Min);
            Assert.AreEqual(2, root.Max);
            Assert.IsNotNull(root.Summary);
            Assert.IsNull(root.Summary.Clusters);
            Assert.IsNull(root.Summary.Summary);
            Assert.AreEqual(1, root.Summary.Min);
            Assert.AreEqual(1, root.Summary.Max);

            Assert.IsNotNull(root.Clusters);
            Assert.AreEqual(-1, root.Clusters[0].Min);
            Assert.AreEqual(-1, root.Clusters[0].Max);
            Assert.IsNull(root.Clusters[0].Clusters);
            Assert.IsNull(root.Clusters[0].Summary);

            Assert.AreEqual(0, root.Clusters[1].Min);
            Assert.AreEqual(0, root.Clusters[1].Max);
            Assert.IsNull(root.Clusters[1].Clusters);
            Assert.IsNull(root.Clusters[1].Summary);
        }

        [Test]
        [Category("H-Insert")]
        public void InsertFull()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(1);
            root.Insert(2);
            root.Insert(3);


            Assert.AreEqual(4, root.Universe);
            Assert.AreEqual(0, root.Min);
            Assert.AreEqual(3, root.Max);
            Assert.IsNotNull(root.Summary);
            Assert.IsNull(root.Summary.Clusters);
            Assert.IsNull(root.Summary.Summary);
            Assert.AreEqual(0, root.Summary.Min);
            Assert.AreEqual(1, root.Summary.Max);

            Assert.IsNotNull(root.Clusters);
            Assert.AreEqual(1, root.Clusters[0].Min);
            Assert.AreEqual(1, root.Clusters[0].Max);
            Assert.IsNull(root.Clusters[0].Clusters);
            Assert.IsNull(root.Clusters[0].Summary);

            Assert.AreEqual(0, root.Clusters[1].Min);
            Assert.AreEqual(1, root.Clusters[1].Max);
            Assert.IsNull(root.Clusters[1].Clusters);
            Assert.IsNull(root.Clusters[1].Summary);
        }


        [Test]
        [Category("H-Insert")]
        public void InsertOddPower()
        {
            VebTreeNode root = new VebTreeNode(8);
            root.Insert(4);
            root.Insert(6);
            root.Insert(2);
            root.Insert(3);
            Assert.AreEqual(2, root.Min);
            Assert.AreEqual(6, root.Max);

            Assert.IsNotNull(root.Summary);
            Assert.AreEqual(4, root.Summary.Universe);
            Assert.AreEqual(1, root.Summary.Min);
            Assert.AreEqual(3, root.Summary.Max);

            Assert.IsNotNull(root.Summary.Summary);
            Assert.AreEqual(2, root.Summary.Summary.Universe);
            Assert.AreEqual(1, root.Summary.Summary.Min);
            Assert.AreEqual(1, root.Summary.Summary.Max);
            Assert.IsNull(root.Summary.Summary.Summary);
            Assert.IsNull(root.Summary.Summary.Clusters);

            Assert.AreEqual(-1, root.Clusters[0].Min);
            Assert.AreEqual(-1, root.Clusters[0].Max);
            Assert.IsNull(root.Clusters[0].Summary);

            Assert.AreEqual(1, root.Clusters[1].Min);
            Assert.AreEqual(1, root.Clusters[1].Max);
            Assert.IsNull(root.Clusters[1].Summary);

            Assert.AreEqual(0, root.Clusters[2].Min);
            Assert.AreEqual(0, root.Clusters[2].Max);
            Assert.IsNull(root.Clusters[2].Summary);

            Assert.AreEqual(0, root.Clusters[3].Min);
            Assert.AreEqual(0, root.Clusters[3].Max);
            Assert.IsNull(root.Clusters[3].Summary);
        }

        [Test]
        [Category("H-Insert")]
        public void InsertSize16()
        {
            VebTreeNode root = new VebTreeNode(16);
            List<int> data = new List<int> { 2, 3, 4, 5, 7, 14, 15 };

            foreach (int k in data)
            {
                root.Insert(k);
            }

            
            NodeChecker(root, "The van Emde Boas tree root", 16, 2, 15, false, 4);

            // Check depth 1.
            NodeChecker(root.Summary, "vEB(4) node 1", 4, 0, 3, false, 2);
            NodeChecker(root.Clusters[0], "vEB(4) node 2", 4, 3, 3, false, 2);
            NodeChecker(root.Clusters[1], "vEB(4) node 3", 4, 0, 3, false, 2);
            NodeChecker(root.Clusters[2], "vEB(4) node 4", 4, -1, -1, false, 2);
            NodeChecker(root.Clusters[3], "vEB(4) node 5", 4, 2, 3, false, 2);

            // Check depth 2.
            NodeChecker(root.Summary.Summary, "vEB(2) node 1", 2, 0, 1, true, 0);
            NodeChecker(root.Summary.Clusters[0], "vEB(2) node 2", 2, 1, 1, true, 0);
            NodeChecker(root.Summary.Clusters[1], "vEB(2) node 3", 2, 1, 1, true, 0);

            NodeChecker(root.Clusters[0].Summary, "vEB(2) node 4", 2, -1, -1, true, 0);
            NodeChecker(root.Clusters[0].Clusters[0], "vEB(2) node 5", 2, -1, -1, true, 0);
            NodeChecker(root.Clusters[0].Clusters[1], "vEB(2) node 6", 2, -1, -1, true, 0);

            NodeChecker(root.Clusters[1].Summary, "vEB(2) node 7", 2, 0, 1, true, 0);
            NodeChecker(root.Clusters[1].Clusters[0], "vEB(2) node 8", 2, 1, 1, true, 0);
            NodeChecker(root.Clusters[1].Clusters[1], "vEB(2) node 9", 2, 1, 1, true, 0);

            NodeChecker(root.Clusters[2].Summary, "vEB(2) node 10", 2, -1, -1, true, 0);
            NodeChecker(root.Clusters[2].Clusters[0], "vEB(2) node 11", 2, -1, -1, true, 0);
            NodeChecker(root.Clusters[2].Clusters[1], "vEB(2) node 12", 2, -1, -1, true, 0);

            NodeChecker(root.Clusters[3].Summary, "vEB(2) node 13", 2, 1, 1, true, 0);
            NodeChecker(root.Clusters[3].Clusters[0], "vEB(2) node 14", 2, -1, -1, true, 0);
            NodeChecker(root.Clusters[3].Clusters[1], "vEB(2) node 15", 2, 1, 1, true, 0);
        }

        /// <summary>
        /// credit for this method goes to Xyaneon
        /// </summary>
        /// <param name="node"></param>
        /// <param name="universe"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="summaryIsNull"></param>
        /// <param name="clusterSize"></param>
        private void NodeChecker(VebTreeNode node,
            string nodeName,
            int expectedUniverse,
            int expectedMinimum,
            int expectedMaximum,
            bool shouldSummaryBeNull,
            int expectedClusterSize)
        {
            
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Assert.AreEqual(
                expectedUniverse,
                node.Universe,
                $"{nodeName} claims a universe size of {node.Universe} instead of {expectedUniverse}.");

            Assert.AreEqual(
                expectedMinimum,
                node.Min,
                $"{nodeName} claims a minimum of {node.Min} instead of {expectedMinimum}.");

            Assert.AreEqual(
                expectedMaximum,
                node.Max,
                $"{nodeName} claims a maximum of {node.Max} instead of {expectedMaximum}.");

            if (shouldSummaryBeNull)
            {
                Assert.IsNull(
                    node.Summary,
                    $"{nodeName} has a non-null summary pointer.");
            }
            else
            {
                Assert.IsNotNull(
                    node.Summary,
                    $"{nodeName} has a null summary pointer.");
            }

            if (expectedClusterSize == 0)
            {
                Assert.IsNull(
                node.Clusters,
                $"{nodeName} has a non-null cluster list pointer.");
            }
            else
            {
                Assert.IsNotNull(
                    node.Clusters,
                    $"{nodeName} has a null cluster list pointer.");

                int actualClusterSize = node.Clusters.Length;
                Assert.AreEqual(
                    expectedClusterSize,
                    actualClusterSize,
                    $"{nodeName} has {actualClusterSize} element(s) instead of {expectedClusterSize}.");
            }
        }


        [Test]
        [Category("I-Find")]
        public void FindSingle()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(1);
            Assert.IsTrue(root.Find(1));
        }

        [Test]
        [Category("I-Find")]
        public void FindTwo()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(2);

            Assert.IsTrue(root.Find(0));
            Assert.IsTrue(root.Find(2));
        }

        [Test]
        [Category("I-Find")]
        public void FindFull()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(1);
            root.Insert(2);
            root.Insert(3);

            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(root.Find(i));
            }
        }


        [Test]
        [Category("I-Find")]
        public void FindOddPower()
        {
            List<int> keys = new List<int> { 4, 6, 2, 3 };
            VebTreeNode root = new VebTreeNode(8);
            foreach(int key in keys)
            {
                root.Insert(key);
            }
            foreach (int key in keys)
            {
                Assert.IsTrue(root.Find(key));
            }
        }

        [Test]
        [Category("I-Find")]
        public void FindNone()
        {
            VebTreeNode root = new VebTreeNode(8);
            root.Insert(2);
            root.Insert(3);
            root.Insert(4);
            root.Insert(6);
            Assert.IsFalse(root.Find(1));
            Assert.IsFalse(root.Find(7));
            Assert.IsFalse(root.Find(8));
            Assert.IsFalse(root.Find(10));
            Assert.IsFalse(root.Find(5));
        }

        [Test]
        [Category("J-Successor")]
        public void NoSuccessor()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(1);
            Assert.AreEqual(-1, root.Successor(1));
        }

        [Test]
        [Category("J-Successor")]
        public void SuccessorTwo()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(2);

            Assert.AreEqual(2, root.Successor(0));
            Assert.AreEqual(2, root.Successor(1));
        }

        [Test]
        [Category("J-Successor")]
        public void SuccessorFull()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(1);
            root.Insert(2);
            root.Insert(3);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(i + 1, root.Successor(i));
            }
        }

        [Test]
        [Category("J-Successor")]
        public void SuccessorOddPower()
        {
            VebTreeNode root = new VebTreeNode(8);
            root.Insert(2);
            root.Insert(3);
            root.Insert(4);
            root.Insert(6);

            Assert.AreEqual(6, root.Successor(4));
            Assert.AreEqual(2, root.Successor(1));
            Assert.AreEqual(3, root.Successor(2));
        }

        [Test]
        [Category("J-Successor")]
        public void Successor16()
        {
            VebTreeNode root = new VebTreeNode(16);
            List<int> data = new List<int> { 2, 3, 4, 5, 6, 14, 15 };

            foreach (int k in data)
            {
                root.Insert(k);
            }

            Assert.AreEqual(2, root.Min);
            Assert.AreEqual(15, root.Max);

            for (int i = 0; i < data.Count - 1; i++)
            {
                Assert.IsTrue(root.Find(data[i]));
                Assert.AreEqual(data[i + 1], root.Successor(data[i]));
            }

            Assert.AreEqual(-1, root.Successor(15));
        }


        [Test]
        [Category("K-Remove")]
        public void RemoveMin()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(1);
            root.Remove(1);
            Assert.AreEqual(-1, root.Min);
            Assert.AreEqual(-1, root.Max);
        }

        [Test]
        [Category("K-Remove")]
        public void RemoveMinTwo()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(1);
            root.Insert(2);
            root.Remove(1);
            Assert.AreEqual(2, root.Min);
            Assert.AreEqual(2, root.Max);
        }

        [Test]
        [Category("K-Remove")]
        public void RemoveFullMin()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(1);
            root.Insert(2);
            root.Insert(3);

            root.Remove(0);

            Assert.AreEqual(1, root.Min);
            Assert.AreEqual(3, root.Max);
        }

        [Test]
        [Category("K-Remove")]
        public void RemoveFullMax()
        {
            VebTreeNode root = new VebTreeNode(4);
            root.Insert(0);
            root.Insert(1);
            root.Insert(2);
            root.Insert(3);

            root.Remove(3);

            Assert.AreEqual(0, root.Min);
            Assert.AreEqual(2, root.Max);
        }

        [Test]
        [TestCase(2, 3, 15)]
        [TestCase(15, 2, 14)]
        [Category("K-Remove")]
        public void Remove16(int key, int min, int max)
        {
            VebTreeNode root = new VebTreeNode(16);
            List<int> data = new List<int> { 2, 3, 4, 5, 6, 14, 15 };

            foreach (int k in data)
            {
                root.Insert(k);
            }
            root.Remove(key);
            Assert.AreEqual(min, root.Min);
            Assert.AreEqual(max, root.Max);
        }

        [Test]
        [Category("K-Remove")]
        public void RemoveSuccessor()
        {
            VebTreeNode root = new VebTreeNode(16);
            List<int> data = new List<int> { 2, 3, 4, 5, 6, 14, 15 };

            foreach (int k in data)
            {
                root.Insert(k);
            }

            root.Remove(15);
            Assert.AreEqual(-1, root.Successor(14));
            Assert.AreEqual(14, root.Successor(7));
            root.Remove(14);
            Assert.AreEqual(-1, root.Successor(7));
            root.Remove(5);
            Assert.AreEqual(6, root.Successor(4));
            root.Remove(2);
            Assert.AreEqual(3, root.Successor(1));
        }

        [Test, Timeout(10000)]
        [Category("Z-Torture")]
        public void VebTreeLarge()
        {
            int max = 524288;
            VebTreeNode root = new VebTreeNode(max);
            List<int> data = Enumerable.Range(0, max).ToList();

            foreach (int k in data)
            {
                root.Insert(k);
            }

            Assert.AreEqual(0, root.Min);
            Assert.AreEqual(max - 1, root.Max);

            for (int i = 0; i < data.Count - 1; i++)
            {
                Assert.IsTrue(root.Find(data[i]));
                Assert.AreEqual(data[i + 1], root.Successor(data[i]));
            }

            foreach (int k in data)
            {
                root.Remove(k);
            }
        }
        [Test, Timeout(15000)]
        [Category("Z-Torture")]
        public void VebTreeHuge()
        {
            int max = 1048576;
            VebTreeNode root = new VebTreeNode(max);
            List<int> data = Enumerable.Range(0, max).ToList();

            foreach (int k in data)
            {
                root.Insert(k);
            }

            Assert.AreEqual(0, root.Min);
            Assert.AreEqual(max-1, root.Max);

            for (int i = 0; i < data.Count-1; i++)
            {
                Assert.IsTrue(root.Find(data[i]));
                Assert.AreEqual(data[i+1], root.Successor(data[i]));
            }

            foreach (int k in data)
            {
                root.Remove(k);
            }
        }        
    }
}
