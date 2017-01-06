using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Common.Extension
{
    public static class BoolExt
    {
        public static bool WhenTrue(this bool val, Action action)
        {
            if (val)
            {
                action();
            }
            return val;
        }

        public static bool WhenFalse(this bool val, Action action)
        {
            if (!val)
            {
                action();
            }
            return val;
        }
    }
}
