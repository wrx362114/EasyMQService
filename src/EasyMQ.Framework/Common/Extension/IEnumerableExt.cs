using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMQ.Framework.Common.Extension
{ 
    public static class IEnumerableExt
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
            return list;
        }

        public static IEnumerable<T> NotDefault<T>(this IEnumerable<T> list)
        {
            return list.Where(m => m != null);
        }

        public static int Index<T>(this IEnumerable<T> list, T item)
        {
            int i = 0;
            foreach (var l in list)
            {
                if (l.Equals(item))
                {
                    break;
                }
            }
            return i;
        }
    }
}
