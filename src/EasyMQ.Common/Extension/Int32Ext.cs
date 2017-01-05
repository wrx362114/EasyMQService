using System;

namespace EasyMQ.Common.Extension
{
    public static class Int32Ext
	{
		private static readonly Random _random = new Random();

		public static int Random(this int min, int max)
		{
			return _random.Next(min, max);
		}

		public static void For(this int count, Action<int> action)
		{
			for (int i = 0; i < count; i++)
			{
				action(i);
			}
		}
		public static void For(this int count, Action action)
		{
			for (int i = 0; i < count; i++)
			{
				action();
			}
		}
	}
}
