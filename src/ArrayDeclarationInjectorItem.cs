/* -----------------------------------------------------------
ContentInjector for ASP.NET MVC
Copyright (C) 2013  Peter L. Blum

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;

namespace ContentInjector
{
/// <summary>
/// Defines one variable with a list of values used to generate the array.
/// </summary>
   public class ArrayDeclarationInjectorItem : IScriptBlockInjectorItem
   {
      public ArrayDeclarationInjectorItem(string variableName)
      {
         VariableName = variableName;
         Elements = new List<string>();
      }

/// <summary>
/// A JavaScript compatible variable name.
/// </summary>
      public string VariableName
      {
         get { return _variableName; }
         set
         {
            if (String.IsNullOrEmpty(value))
               throw new ArgumentNullException();
            _variableName = value;
         }
      }
      private string _variableName;

/// <summary>
/// A list of JavaScript compatible values that appear in an array.
/// They will appear in the order of this list.
/// </summary>
/// <remarks>
/// <para>All elements are in the exact form they will be output.
/// Use the various Add() methods to add to it, converting correctly
/// from the real type into a string.</para>
/// </remarks>
      public List<string> Elements { get; protected set; }

/// <summary>
/// Add an integer.
/// </summary>
/// <param name="value"></param>
      public void Add(int value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }
      
/// <summary>
/// Add a short integer.
/// </summary>
/// <param name="value"></param>
      public void Add(short value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }

/// <summary>
/// Add a 64 bit integer.
/// </summary>
/// <param name="value"></param>
      public void Add(Int64 value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }

/// <summary>
/// Add a double.
/// </summary>
/// <param name="value"></param>
      public void Add(double value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }

/// <summary>
/// Add a single.
/// </summary>
/// <param name="value"></param>
      public void Add(Single value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }

/// <summary>
/// Add a decimal.
/// </summary>
/// <param name="value"></param>
      public void Add(decimal value)
      {
         Elements.Add(value.ToString(CultureInfo.InvariantCulture));
      }

/// <summary>
/// Adds a string. It will be enclosed in double-quotes and Html encoded
/// unless the htmlEncode parameter is false.
/// </summary>
/// <param name="value"></param>
/// <param name="htmlEncode">Ensures html encoding. If not specified, it defaults to true.</param>
      public void Add(string value, bool htmlEncode = true)
      {
         if (htmlEncode)
            Elements.Add("\"" + HttpUtility.HtmlEncode(value) + "\"");
         else
            Elements.Add("\"" + value.Replace("\"", "&quot;") + "\"");
      }

/// <summary>
/// Use when inserting a string representing javascript code. It
/// will output as is.
/// </summary>
/// <param name="value"></param>
      public void AddCode(string value)
      {
         Elements.Add(value);
      }

/// <summary>
/// Add a decimal.
/// </summary>
/// <param name="value"></param>
      public void Add(bool value)
      {
         Elements.Add(value ? "true" : "false");
      }

      #region IKeyedInjectorItem Members

      public string GetKey()
      {
         return VariableName;
      }

      public void SetKey(string key)
      {
         VariableName = key;
      }

      #endregion

      public string GetScript()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("var ");
         sb.Append(VariableName);
         sb.Append(" = [");
         bool first = true;
         foreach (string element in Elements)
         {
            if (!first)
               sb.Append(", ");
            first = false;
            sb.Append(element);
         }
         sb.AppendLine("];");
         return sb.ToString();
      }

   }
}
