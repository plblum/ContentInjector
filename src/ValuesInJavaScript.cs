using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;

namespace ContentInjector
{

/// <summary>
/// Utility class that creates strings representing the values
/// for use in javascript, such as in objects and arrays.
/// Call the ToScript() method with a strongly typed value
/// and get back a string that will become the same type
/// when injected into javascript.
/// </summary>
   public class ValuesInJavaScript
   {

/// <summary>
/// Convert an integer.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(int value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }
      
/// <summary>
/// Convert a short integer.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(short value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }

/// <summary>
/// Convert a 64 bit integer.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(Int64 value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }

/// <summary>
/// Convert a double.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(double value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }

/// <summary>
/// Convert a single.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(Single value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }

/// <summary>
/// Convert a decimal.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(decimal value)
      {
         return value.ToString(CultureInfo.InvariantCulture);
      }

/// <summary>
/// Converts a string. It will be enclosed in double-quotes and Html encoded
/// unless the htmlEncode parameter is false.
/// </summary>
/// <param name="value"></param>
/// <param name="htmlEncode">Ensures html encoding. If not specified, it defaults to true.</param>
      public static string ToScript(string value, bool htmlEncode = true)
      {
         if (htmlEncode)
            return "\"" + HttpUtility.HtmlEncode(value) + "\"";
         else
            return "\"" + value.Replace("\"", "&quot;") + "\"";
      }

/// <summary>
/// Convert a boolean.
/// </summary>
/// <param name="value"></param>
      public static string ToScript(bool value)
      {
         return value ? "true" : "false";
      }

/// <summary>
/// Use when inserting a string representing javascript code. It
/// will output as is.
/// </summary>
/// <param name="value"></param>
      public static string AsCode(string value)
      {
         return value;
      }

/// <summary>
/// Returns a value for null
/// </summary>
/// <returns></returns>
      public static string AsNull()
      {
         return "null";
      }

      public static string ToScript(object value)
      {
         if (value == null)
            return AsNull();
         if (value is string)
            return ToScript((string)value);  // defaults to HTML Encode
         if (value is bool)
            return ToScript((bool)value);
         if (value is int)
            return ToScript((int)value);
         if (value is short)
            return ToScript((short)value);
         if (value is Int64)
            return ToScript((Int64)value);
         if (value is double)
            return ToScript((double)value);
         if (value is decimal)
            return ToScript((decimal)value);
         if (value is Single)
            return ToScript((Single)value);
         throw new ArgumentException(String.Format("Cannot convert the value type {0} into a JavaScript native type.", value.GetType().FullName));
      }

   }
}
