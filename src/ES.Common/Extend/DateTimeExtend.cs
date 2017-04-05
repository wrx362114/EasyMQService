using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Common.Extend
{
    /// <summary>
    /// 日期相关扩展
    /// </summary>
    public static class DateTimeExtend
    {
        /// <summary>
        /// (扩展)可空日期格式化
        /// </summary>
        /// <param name="source">可空日期</param>
        /// <param name="format">格式化字符串</param>
        /// <returns></returns>
        public static string ToString(this DateTime? source, string format = null)
        {
            if (source.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(format))
                {
                    return source.Value.ToString(format);
                }
                else
                {
                    return source.Value.ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
