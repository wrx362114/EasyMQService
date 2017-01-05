using System;
using System.Xml.Serialization;
using System.IO;
namespace EasyMQ.Common.Extension
{
    public static class ObjectExt
    {
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        public static string ToXml(this object obj)
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

        /// <summary> 转换对象 </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="obj">要转换的对象</param>
        /// <param name="convert">转换方法</param>
        public static T Convert<T>(this object obj, Func<object, T> convert)
        {
            return convert(obj);
        }

        public static T Max<T>(this T obj, T obj2) where T : IComparable<T>
        {
            return obj.CompareTo(obj2) > 0 ? obj : obj2;
        }
        public static T Min<T>(this T obj, T obj2) where T : IComparable<T>
        {
            return obj.CompareTo(obj2) < 0 ? obj : obj2;
        }
    }
}
