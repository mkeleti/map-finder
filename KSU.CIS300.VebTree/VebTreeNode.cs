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
            if (universeSize == 0 || ((universeSize) ^ (universeSize - 1)) != 0)
            {
                throw new ArgumentException();
            }
            else
            {
                Universe = universeSize;
                _power = (int)Math.Pow(universeSize, 1 / 2);
                Min = -1;
                Max = -1;
                if (Universe > 2)
                {
                    int UpperSquare = UpperSquareRoot();
                    Summary.Universe = UpperSquare;
                    Clusters = new VebTreeNode[UpperSquare];
                }
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
            int ReturnValue = key / (int)LowerSquareRoot();
            return ReturnValue;
        }

        public int Low(int key)
        {
            int ReturnValue = key % (int)LowerSquareRoot();
            return ReturnValue;
        }

        public int Index(int subtree, int offset)
        {
            int ReturnValue = subtree * (int)LowerSquareRoot() + offset;
            return ReturnValue;
        }

        public void Insert(int key)
        {
            if (Min == -1)
            {
                Min = key;
                Max = key;
            }
            else
            {
                if (key < Min)
                {
                    Min = key;
                }
                if (Universe > 2)
                {
                    // Add more recursion
                    int Highvalue = High(key);
                    int Lowvalue = Low(key);
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
            else
            {
                // Double Check
                Find(High(key));
            }
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
            else if (key > 1 && key < Min)
            {

                return Min;

            }
            else
            {
                int Index = High(key);
                Clusters[Index].Find(Max);
            }

               
        }

        public void Remove(int keyToRemove)
        {

        }
    }
}
