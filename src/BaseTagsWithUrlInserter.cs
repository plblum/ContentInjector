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
/// Creates a single HTML tag that hosts a URL, such as a script or link tag.
/// </summary>
/// <remarks>
/// </remarks>
   public abstract class BaseTagsWithUrlInserter : BaseKeyedInserter<UrlInserterItem>
   {
#if false
      #region IInserter Members

      public override string GetContent(HttpContextBase httpContext)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var orders in _orderedList)
         {
            foreach (UrlInserterItem items in orders.Value)
            {
               sb.AppendLine(GetTag(items.Url, httpContext));
            }
         }

         return sb.ToString();
      }
      #endregion
#endif
      protected override void ItemContent(UrlInserterItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(GetTag(item.Url, httpContext));
      }

/// <summary>
/// Creates the HTML tag based on the URL.
/// </summary>
/// <param name="url">NOTE: url is already converted from virtual to absolute path</param>
/// <returns></returns>
      protected abstract string GetTag(string url, HttpContextBase httpContext);

      protected override IComparer<string> GetComparer()
      {
         return StringComparer.OrdinalIgnoreCase;
      }

/// <summary>
/// Adds with the default order but does not add a duplicate URL (case insensitive match).
/// </summary>
/// <param name="url"></param>
      public void Add(string url)
      {
         Add(url, 0);
      }


/// <summary>
/// Adds but does not add a duplicate URL (case insensitive match).
/// </summary>
/// <param name="url"></param>
/// <param name="order"></param>
      public virtual void Add(string url, int order)
      {
         url = VirtualPathUtility.ToAbsolute(url);
         if (!Contains(url))
            Add(new UrlInserterItem(url), order);

      }
   }

}
