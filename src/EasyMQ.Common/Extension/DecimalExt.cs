using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Common.Extension
{
    public static class DecimalExt
    {
        public static decimal RoundMoney(this decimal dec)
        {
            return decimal.Parse(dec.ToString(".00"));
        }
    }
}
