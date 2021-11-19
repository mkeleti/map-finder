/* MinPriorityQueue.cs
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
    public class MinPriorityQueue
    {
        /// <summary>
        /// Stores all the elements in the VebTreeNode
        /// </summary>
        private VebTreeNode _elements;
        /// <summary>
        /// The minimum value in the priority queue.
        /// </summary>
        public int MinimumPriority
        {
            get
            {
                if (_elements == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return _elements.Min;
                }
            }
        }
        /// <summary>
        /// How many elements are in the priority queue.
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Constructor for the minimum priority queue.
        /// </summary>
        /// <param name="maxSize">The maximum size of the queue</param>
        public MinPriorityQueue(int maxSize)
        {
            _elements = new VebTreeNode(maxSize);
        }
        /// <summary>
        /// Adds an element to the queue.
        /// </summary>
        /// <param name="num">Number to add to the queue.</param>
        public void Enqueue(int num)
        {
            _elements.Insert(num);
            Count++;
        }
        /// <summary>
        /// Removes an element from the queue.
        /// </summary>
        /// <returns>The key that is removed from the queue.</returns>
        public int Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            int ReturnValue = MinimumPriority;
            Count--;
            _elements.Remove(ReturnValue);
            return ReturnValue;
        }
    }
}
