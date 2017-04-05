using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ES.Common.Extend
{
    /// <summary>
    /// 字节数组扩展方法
    /// </summary>
    public static class BytesExtend
    {
        static MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encode2String(this byte[] data)
        {
            return GlobalConfig.Encoding.GetString(data);
        }
        /// <summary>
        /// 转字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encode2String(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
        /// <summary>
        /// md5获取
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5Hex(this byte[] data)
        {
            return MD5Provider.ComputeHash(data).Select(m => m.ToString("X2")).JoinStr("");
        }
    }
}
