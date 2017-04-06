using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Common.Extend
{
    /// <summary>
    /// 数组扩展
    /// </summary>
    public static class CollectionsExtend
    {

        /// <summary>
        /// 字符串连接
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="splitString"></param>
        /// <returns></returns>
        public static string JoinStr(this IEnumerable<string> strs, string splitString = ",")
        {
            return string.Join(splitString, strs);
        }
    }
}
