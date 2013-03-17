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
/// Creates a single HTML tag that hosts a URL, such as a script or link tag.
/// </summary>
/// <remarks>
/// </remarks>
   public class MetaTagsInjector : BaseKeyedInjector<MetaTagInjectorItem>, IMetaTagsInjector
   {
#if false
      #region IInjector Members

      public override string GetContent(HttpContextBase httpContext)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var orders in _orderedList)
         {
            foreach (MetaTagInjectorItem items in orders.Value)
            {
               sb.AppendLine(String.Format(MetaTagPattern, items.Name, items.Content));
            }
         }

         return sb.ToString();
      }
      #endregion
#endif

      protected override void ItemContent(MetaTagInjectorItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         string usage = "name";
         switch (item.Usage)
         {
            case MetaTagUsage.CharSet:
               usage = "charset";
               break;
            case MetaTagUsage.HttpEquiv:
               usage = "http-equiv";
               break;
         }
         sb.AppendLine(String.Format(MetaTagPattern, item.Name, item.Content, usage));
      }

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the Name is inserted and {1} where Content is inserted.
/// {2} for the attribute hosting the Name.
/// </summary>
      public static string MetaTagPattern = "<meta {2}=\"{0}\" content=\"{1}\" />";


      protected override IComparer<string> GetComparer()
      {
         return StringComparer.OrdinalIgnoreCase;
      }

/// <summary>
/// Adds with the default order but does not add a duplicate Name (case insensitive match).
/// </summary>
/// <param name="name"></param>
/// <param name="content"></param>
      public void Add(string name, string content)
      {
         Add(name, content, 0);
      }


/// <summary>
/// Adds but does not add a duplicate Name (case insensitive match).
/// </summary>
/// <param name="name"></param>
/// <param name="content"></param>
/// <param name="order"></param>
      public virtual void Add(string name, string content, int order)
      {
         if (!Contains(name))
            Add(new MetaTagInjectorItem(name, content), order);

      }

/// <summary>
/// Adds but does not add a duplicate Name (case insensitive match).
/// Allows changing the usage.
/// </summary>
/// <param name="usage"></param>
/// <param name="name"></param>
/// <param name="content"></param>
/// <param name="order"></param>
      public virtual void Add(MetaTagUsage usage, string name, string content, int order)
      {
         if (!Contains(name))
            Add(new MetaTagInjectorItem(usage, name, content), order);
      }

   }

}
