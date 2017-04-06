using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.Common;

namespace ES.Common.Extend
{
    /// <summary>
    /// 基本对象扩展
    /// </summary>
    public static class ObjectExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange<T>(this T v, T min, T max) where T : IComparable
        {
            return v.CompareTo(min) >= 0 && v.CompareTo(max) <= 0;
        }
    }
}
