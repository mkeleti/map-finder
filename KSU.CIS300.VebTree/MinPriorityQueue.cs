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
        /// 
        /// </summary>
        private VebTreeNode _elements;
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxSize"></param>
        public MinPriorityQueue(int maxSize)
        {
            _elements = new VebTreeNode(maxSize);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        public void Enqueue(int num)
        {
            _elements.Insert(num);
            Count++;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            int num = MinimumPriority;
            _elements.Remove(num);
            Count--;
            return num;
        }
    }
}
