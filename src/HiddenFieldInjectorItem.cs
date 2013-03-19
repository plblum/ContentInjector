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
using System.Web;

namespace ContentInjector
{
/// <summary>
/// Data used by the HiddenFieldsInjector to hold the name and value
/// attribute values for a input type='hidden' tag.
/// </summary>
   public class HiddenFieldInjectorItem : IKeyedInjectorItem
   {
      public HiddenFieldInjectorItem(string name, string value)
      {
         Name = name;
         Value = value ?? String.Empty;
      }

/// <summary>
/// Assigned to the input tag's [name] attribute.
/// </summary>
      public string Name
      {
         get { return _name; }
         set
         {
            if (String.IsNullOrEmpty(value))
               throw new ArgumentException("Must contain some text.");
            _name = value;
         }
      }
      private string _name;

/// <summary>
/// Assigned to the input tag's [value] attribute.
/// </summary>
      public string Value { get; set; }

      #region IKeyedInjectorItem Members

      string IKeyedInjectorItem.GetKey()
      {
         return Name;
      }

      void IKeyedInjectorItem.SetKey(string key)
      {
         Name = key;
      }

/// <summary>
/// Existing item's value is changed.
/// </summary>
/// <param name="item"></param>
/// <returns>False</returns>
      public virtual bool Merge(IKeyedInjectorItem item)
      {
         Value = ((HiddenFieldInjectorItem)item).Value;
         return true;
      }


      #endregion

/// <summary>
/// Returns the string representing the hidden field.
/// </summary>
/// <returns></returns>
      public virtual string GetContent(HttpContextBase context)
      {
         return String.Format(HiddenFieldFormat, Name, Value);
      }

/// <summary>
/// Provides the format used by GetContent with String.Format() to create
/// the HTML for the hidden field.
/// </summary>
/// <value>
/// <para>When unassigned, it uses the default in the static field DefaultHiddenFieldFormat.</para>
/// </value>
      public string HiddenFieldFormat
      {
         get { return _hiddenFieldFormat ?? DefaultHiddenFieldFormat; }
         set { _hiddenFieldFormat = value; }
      }
      private string _hiddenFieldFormat;

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the Name is inserted and {1} where Value is inserted.
/// </summary>
      public static string DefaultHiddenFieldFormat = "<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />";

   }
}
