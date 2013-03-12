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
using System.Web;

namespace InsertionsManagement
{
/// <summary>
/// Creates input type='hidden' fields. Use to group these field types together.
/// </summary>
/// <remarks>
/// Be sure to place the insertion point into the markup within a form tag block.
/// </remarks>
   public class HiddenFieldsInserter : BaseKeyedInserter<HiddenFieldInserterItem>, IHiddenFieldsInserter
   {
#if false
      #region IInserter Members

      public override string GetContent(HttpContextBase httpContext)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var orders in _orderedList)
         {
            foreach (HiddenFieldInserterItem items in orders.Value)
            {
               sb.AppendLine(String.Format(HiddenFieldPattern, items.Name, items.Value));
            }
         }

         return sb.ToString();
      }
      #endregion
#endif
      protected override void ItemContent(HiddenFieldInserterItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(String.Format(HiddenFieldPattern, item.Name, item.Value));
      }

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the Name is inserted and {1} where Value is inserted.
/// </summary>
      public static string HiddenFieldPattern = "<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />";


      protected override IComparer<string> GetComparer()
      {
         return StringComparer.OrdinalIgnoreCase;
      }

/// <summary>
/// Adds with the default order but does not add a duplicate Name (case insensitive match).
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
      public void Add(string name, string value)
      {
         Add(name, value, 0);
      }


/// <summary>
/// Adds but does not add a duplicate Name (case insensitive match).
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public virtual void Add(string name, string value, int order)
      {
         if (!Contains(name))
            Add(new HiddenFieldInserterItem(name, value), order);

      }
   }

}
