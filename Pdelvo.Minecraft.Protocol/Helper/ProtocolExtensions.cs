using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pdelvo.Minecraft.Protocol.Helper
{
    public static class ProtocolExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (action == null)
                throw new ArgumentNullException("action");
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
