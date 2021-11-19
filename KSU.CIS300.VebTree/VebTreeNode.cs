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
            Universe = universeSize;
            _power = (int)Math.Log(Universe, 2);

            Min = -1;
            Max = -1;

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
        /// <returns></returns>
        public double LowerSquareRoot()
        {

            return Math.Pow(2, _power / 2);
        }
        /// <summary>
        /// Calculates the lower square root of the Universe and casts as an integer.
        /// </summary>
        /// <returns></returns>
        public int UpperSquareRoot()
        {


            return (int)Math.Pow(2, (_power + 1) / 2);
        }

        public int High(int key)
        {
            int _high = (int)Math.Floor(key / LowerSquareRoot());
            return _high;
        }

        public int Low(int key)
        {
            int _low = key % (int)LowerSquareRoot();
            return _low;
        }

        public int Index(int subtree, int offset)
        {
            int _index = subtree * (int)LowerSquareRoot() + offset;
            return _index;
        }

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
                    int highKey = High(key);
                    int lowKey = Low(key);

                    if (Clusters[highKey].Min == -1)
                    {
                        Summary.Insert(highKey);
                    }
                    Clusters[highKey].Insert(lowKey);
                }
                if (key > Max)
                {
                    Max = key;
                }
            }
        }

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
            int highKey = High(key);
            int lowKey = Low(key);
            return Clusters[highKey].Find(lowKey);
        }

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
            if (Universe != 2)
            {
                if (key > -1 && key < Min)
                {
                    return Min;
                }
            }

            int highKey = High(key);
            int lowKey = Low(key);
            if (Clusters[highKey].Max != -1 && lowKey < Clusters[highKey].Max) //Clusters[highKey].Max > lowKey)
            {
                int _offset = Clusters[highKey].Successor(lowKey);
                return Index(highKey, _offset);
            }

            else
            {
                int successorClusterIndex = Summary.Successor(highKey);
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
                int highKey = High(keyToRemove);
                int lowKey = Low(keyToRemove);

                Clusters[highKey].Remove(lowKey);

                if (Clusters[highKey].Max == -1)
                {
                    Summary.Remove(highKey);

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
                        Max = Index(highKey, Clusters[highKey].Max);
                    }
                }
            }
        }
    }
}
