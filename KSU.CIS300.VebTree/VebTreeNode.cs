/* VebTreeNode.cs
 * Author: Michael Keleti
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.CIS300.VebTree
{
    /// <summary>
    /// 
    /// </summary>
    public class VebTreeNode
    {
        /// <summary>
        /// Stores the smallest key in this tree.
        /// </summary>
        public int Min
        {
            get;
            private set;
        }
        /// <summary>
        /// Stores the largest key in this tree.
        /// </summary>
        public int Max
        {
            get;
            private set;
        }
        /// <summary>
        /// Stores subtrees that correspond to clusters of bits in the vEB Tree.
        /// </summary>
        public VebTreeNode[] Clusters
        {
            get;
            private set;
        }
        /// <summary>
        /// Stores an auxiliary tree that keeps track of which clusters contain elements.
        /// </summary>
        public VebTreeNode Summary
        {
            get;
            private set;
        }
        /// <summary>
        /// Keeps track of maximum number of bits/keys this tree can contain.
        /// </summary>
        public int Universe
        {
            get;
            private set;
        }
        /// <summary>
        /// Stores the power of 2 the tree's universe.
        /// </summary>
        private int _power;
        /// <summary>
        /// Constructor for VebTreeNode
        /// </summary>
        /// <param name="universeSize"></param>
        public VebTreeNode(int universeSize)
        {
            if (universeSize == 0)
            {
                throw new ArgumentException();
            }
            else if ((universeSize & (universeSize - 1)) != 0)
            {
                throw new ArgumentException();
            }
            Min = -1;
            Max = -1;

            Universe = universeSize;
            _power = (int)Math.Log(Universe, 2);

            if (!(universeSize <= 2))
            {
                Clusters = new VebTreeNode[UpperSquareRoot()];
                Summary = new VebTreeNode(UpperSquareRoot());
                int lower = (int)LowerSquareRoot();

                for (int i = 0; i < Clusters.Length; i++)
                {
                    Clusters[i] = new VebTreeNode(lower);
                }
            }
            else
            {
                Clusters = null;
                Summary = null;
            }
        }
        /// <summary>
        /// Calculates the lower square root of the Universe.
        /// </summary>
        /// <returns>The lower square root of the universe</returns>
        public double LowerSquareRoot()
        {
            return Math.Pow(2, _power / 2);
        }
        /// <summary>
        /// Calculates the lower square root of the Universe and casts as an integer.
        /// </summary>
        /// <returns>The upper square root of the universe</returns>
        public int UpperSquareRoot()
        {

            return (int)Math.Pow(2, (_power + 1) / 2);
        }
        /// <summary>
        /// Finds which subtree the given key belongs to.
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <returns>Index of the upper subtree</returns>
        public int High(int key)
        {
            int ReturnValue = (int)Math.Floor(key / LowerSquareRoot());
            return ReturnValue;
        }
        /// <summary>
        /// Finds the position of the given key within a lower subtree.
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <returns>Index of the lower subtree</returns>
        public int Low(int key)
        {
            int ReturnValue = key % (int)LowerSquareRoot();
            return ReturnValue;
        }
        /// <summary>
        /// Finds the element from the given subtree and offset tree
        /// </summary>
        /// <param name="subtree">Subtree to search within</param>
        /// <param name="offset">Offset of the value</param>
        /// <returns>Index of the element searched for</returns>
        public int Index(int subtree, int offset)
        {
            int ReturnValue = subtree * (int)LowerSquareRoot() + offset;
            return ReturnValue;
        }
        /// <summary>
        /// Inserts a key into the veb tree at its proper position.
        /// </summary>
        /// <param name="key">Key to insert</param>
        public void Insert(int key)
        {
            if (key < 0 || key >= Universe)
            {
                throw new ArgumentException();
            }

            if (Min < 0)
            {
                Min = key;
                Max = key;
            }
            else
            {
                if (key < Min)
                {
                    int _oldkey = Min;
                    Min = key;
                    key = _oldkey;
                }
                if (Universe > 2)
                {
                    int HighKey = High(key);
                    int LowKey = Low(key);

                    if (Clusters[HighKey].Min == -1)
                    {
                        Summary.Insert(HighKey);
                    }
                    Clusters[HighKey].Insert(LowKey);
                }
                if (key > Max)
                {
                    Max = key;
                }
            }
        }
        /// <summary>
        /// Searches the veb tree in order to determine whether or not the key exists or not already.
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <returns>Whether or not the tree exists</returns>
        public bool Find(int key)
        {
            if (key >= Universe)
            {
                return false;
            }

            if (Min == key || Max == key)
            {
                return true;
            }
            if (Universe == 2)
            {
                return false;
            }
            int HighKey = High(key);
            int LowKey = Low(key);
            return Clusters[HighKey].Find(LowKey);
        }
        /// <summary>
        /// Determines the next largest key stored in the tree after the current tree.
        /// </summary>
        /// <param name="key">Key that we are searching after</param>
        /// <returns>The index of the next largest key</returns>
        public int Successor(int key)
        {
            if (Universe == 2)
            {
                if (key == 0 && Max == 1)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else if (key > -1 && key < Min)
            {
                return Min;
            }

            int HighKey = High(key);
            int LowKey = Low(key);
            if (Clusters[HighKey].Max != -1 && LowKey < Clusters[HighKey].Max)
            {
                int _offset = Clusters[HighKey].Successor(LowKey);
                return Index(HighKey, _offset);
            }

            else
            {
                int successorClusterIndex = Summary.Successor(HighKey);
                if (successorClusterIndex == -1)
                {
                    return -1;
                }
                else
                {
                    return Index(successorClusterIndex, Clusters[successorClusterIndex].Min);
                }
            }
        }
        /// <summary>
        /// Removes a key from the VEB tree by first finding the index of the key then removing.
        /// </summary>
        /// <param name="keyToRemove">Key to remove from the tree.</param>
        public void Remove(int keyToRemove)
        {
            if (Min == Max && keyToRemove == Min)
            {
                Min = -1;
                Max = -1;
            }
            else if (Universe == 2)
            {
                if (keyToRemove == 0)
                {
                    Min = 1;
                    Max = 1;
                }
                else
                {
                    Min = 0;
                    Max = 0;
                }
            }
            else
            {
                if (Min == keyToRemove)
                {
                    Min = Index(Summary.Min, Clusters[Summary.Min].Min);
                    keyToRemove = Min;
                }
                int HighKey = High(keyToRemove);
                int LowKey = Low(keyToRemove);

                Clusters[HighKey].Remove(LowKey);

                if (Clusters[HighKey].Max == -1)
                {
                    Summary.Remove(HighKey);

                    if (Summary.Max == -1)
                    {
                        Max = Min;
                    }
                    else
                    {
                        Max = Index(Summary.Max, Clusters[Summary.Max].Max);
                    }
                }
                else
                {
                    if (keyToRemove == Max)
                    {
                        Max = Index(HighKey, Clusters[HighKey].Max);
                    }
                }
            }
        }
    }
}
