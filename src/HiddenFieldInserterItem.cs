/* -----------------------------------------------------------
InsertionsManager for ASP.NET MVC
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

namespace InsertionsManagement
{
/// <summary>
/// Data used by the HiddenFieldsInserter to hold the name and value
/// attribute values for a input type='hidden' tag.
/// </summary>
   public class HiddenFieldInserterItem : IKeyedInserterItem
   {
      public HiddenFieldInserterItem(string name, string value)
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

      #region IKeyedInserterItem Members

      string IKeyedInserterItem.GetKey()
      {
         return Name;
      }

      void IKeyedInserterItem.SetKey(string key)
      {
         Name = key;
      }

      #endregion
   }
}
