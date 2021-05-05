using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.CommonOperations
{
	public static class ObjectHelper
	{
		/// <summary>
		/// جهت دریافت مقدار یک پراپرتی در آبجکت با کمک اسم آن
		/// </summary>
		/// <param name="objectName">اسم پراپرتی</param>
		/// <param name="objectValue">کل آبجکت</param>
		/// <returns>مقدار پراپرتی</returns>
		public static T getValue<T>(string objectName, object objectValue)
		{
			var value = objectValue.GetType().GetProperty(objectName).GetValue(objectValue, null);
			return (T)value;
		}
	}
}
