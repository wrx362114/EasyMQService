using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMQ.Framework.Common.Extension
{
	public static class TypeExt
	{
		public static bool IsRefType(this Type fieldType)
		{
			return (!fieldType.UnderlyingSystemType.IsValueType)
				&& fieldType != typeof(string);
		}
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}



		public static bool IsNumericType(this Type type)
		{
			if (type != null)
			{
				if (type.IsEnum())
				{
					return true;
				}
				switch (type.GetTypeCode())
				{
					case TypeCode.Object:
						if (!type.IsNullableType())
						{
							if (!type.IsEnum())
							{
								return false;
							}
							return true;
						}
						return Nullable.GetUnderlyingType(type).IsNumericType();

					case TypeCode.SByte:
					case TypeCode.Byte:
					case TypeCode.Int16:
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
					case TypeCode.Decimal:
						return true;
				}
			}
			return false;
		}



		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		public static bool IsNullableType(this Type type)
		{
			return (type.IsGenericType() && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		public static bool IsOrHasGenericInterfaceTypeOf(this Type type, Type genericTypeDefinition)
		{
			if (type.GetTypeWithGenericTypeDefinitionOf(genericTypeDefinition) == null)
			{
				return (type == genericTypeDefinition);
			}
			return true;
		}
		public static Type GetTypeWithGenericTypeDefinitionOf(this Type type, Type genericTypeDefinition)
		{
			foreach (Type type2 in type.GetTypeInterfaces())
			{
				if (type2.IsGeneric() && (type2.GetGenericTypeDefinition() == genericTypeDefinition))
				{
					return type2;
				}
			}
			Type type3 = type.FirstGenericType();
			if ((type3 != null) && (type3.GetGenericTypeDefinition() == genericTypeDefinition))
			{
				return type3;
			}
			return null;
		}
		public static Type[] GetTypeInterfaces(this Type type)
		{
			return type.GetInterfaces();
		}
		public static bool IsGeneric(this Type type)
		{
			return type.IsGenericType;
		}


		public static Type FirstGenericType(this Type type)
		{
			while (type != null)
			{
				if (type.IsGeneric())
				{
					return type;
				}
				type = type.BaseType();
			}
			return null;
		}
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}





	}
}
