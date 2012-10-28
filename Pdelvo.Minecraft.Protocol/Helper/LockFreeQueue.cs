using System.Collections.Generic;
using System.Threading;

namespace Pdelvo.Minecraft.Protocol.Helper
{
    //author: floste
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public class LockFreeQueue<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private Node first;
        /// <summary>
        /// 
        /// </summary>
        private Node last;

        /// <summary>
        /// Initializes a new instance of the <see cref="LockFreeQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LockFreeQueue()
        {
            first = new Node();
            last = first;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <remarks></remarks>
        public int Count
        {
            get
            {
                int i = -1;
                Node l = last;

                while (true)
                {
                    i++;
                    if ((l = l.next) == null) return i;
                }
            }
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <remarks></remarks>
        public void Enqueue(T item)
        {
            var newNode = new Node();
            newNode.item = item;
            Node old = Interlocked.Exchange(ref first, newNode);
            old.next = newNode;
        }

        /// <summary>
        /// Dequeues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool TryDequeue(out T item)
        {
            Node current;
            do
            {
                current = last;
                if (current.next == null)
                {
                    item = default(T);
                    return false;
                }
            } while (current != Interlocked.CompareExchange(ref last, current.next, current));
            item = current.next.item;
            current.next.item = default(T);
            return true;
        }

        public IEnumerable<T> EnumerateItems()
        {
            for (var current = last.next; current != null; current = current.next)
            {
                var value =  current.item;
                if (value != null)
                    yield return value;
            }
        }

        #region Nested type: Node

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        private class Node
        {
            /// <summary>
            /// 
            /// </summary>
            public T item;
            /// <summary>
            /// 
            /// </summary>
            public Node next;
        }

        #endregion
    }
}