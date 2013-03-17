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
/// Inserts the content supplied as is. It does not add any HTML tags.
/// </summary>
/// <remarks>
/// <para>
/// Generally unique insertion tags are defined, each with its own group name,
/// so the parts of the page can add their own content in the right place.
/// For example, an HtmlHelper may want to insert markup for the validation
/// errors nearby and expects the user to place the insertion with the same
/// group name as the ID of the markup it generates.
/// In this case, there is usually just one PlaceholderInjectorItem defined
/// per PlaceholderInjector instance.
/// </para>
/// </remarks>
   public class PlaceholderInjector : BaseKeyedInjector<PlaceholderInjectorItem>, IPlaceholderInjector
   {
#if false
      #region IInjector Members

      public override string GetContent(HttpContextBase httpContext)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var orders in _orderedList)
         {
            foreach (PlaceholderInjectorItem items in orders.Value)
            {
               sb.AppendLine(items.Content);
            }
         }

         return sb.ToString();
      }
      #endregion
#endif

      protected override void ItemContent(PlaceholderInjectorItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(item.Content);
      }

/// <summary>
/// Adds the content. Adds duplicates.
/// </summary>
/// <param name="content"></param>
      public void Add(string content)
      {
         Add(content, 0);
      }


/// <summary>
/// Adds the content. Adds duplicates.
/// </summary>
/// <param name="content"></param>
/// <param name="order"></param>
      public virtual void Add(string content, int order)
      {
         Add(new PlaceholderInjectorItem(content), order);

      }
   }

}
