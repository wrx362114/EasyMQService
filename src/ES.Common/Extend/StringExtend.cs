using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ES.Common.Extend
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtend
    {
        /// <summary>
        /// base64字符串转字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string str)
        {
            return Convert.FromBase64String(str);
        }
        /// <summary>
        /// 转字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Tobytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        /// <summary>
        /// 获取app配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(this string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXml<T>(this string xml)
        {
            return (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(xml));
        }
        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T obj)
        {
            using (var stream = new MemoryStream())
            {
                new XmlSerializer(obj.GetType()).Serialize(stream, obj);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 获取md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(this string str)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                bytes = md5.ComputeHash(bytes);
                return string.Join("", bytes.Select(m => m.ToString("X2")));
            }
        }
        /// <summary>
        /// 获取md5
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetMD5(this string str, Encoding encoding)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = encoding.GetBytes(str); 
                bytes = md5.ComputeHash(bytes);
                return string.Join("", bytes.Select(m => m.ToString("X2")));
            }
        }

    }
}
