using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Common.Extension
{
	public static class ByteExt
	{
		public static byte[] MD5Default(this byte[] bytes)
		{
			return MD5.Create().ComputeHash(bytes);
		}

		public static string ToUTF8String(this byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}

		public static string ToString(this byte[] bytes, Encoding code)
		{
			return code.GetString(bytes);
		}
		public static System.IO.MemoryStream ToStream(this byte[] bytes)
		{
			return new System.IO.MemoryStream(bytes);
		}
	}
}
