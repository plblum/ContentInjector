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
/// Creates an HTML tag that uses a URL, such as a script or link tag.
/// </summary>
/// <remarks>
/// <para>The supporting BaseFileInjectorItem class normally outputs a single
/// HTML tag. But subclasses can collect multiple URLs and other identifiers
/// that are used to construct the final content, which may be one or more 
/// HTML tags.</para>
/// </remarks>
   public abstract class BaseFilesInjector : BaseKeyedInjector<BaseFileInjectorItem>
   {
      protected override void ItemContent(BaseFileInjectorItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(item.GetContent(httpContext));
      }


      protected override IComparer<string> GetComparer()
      {
         return StringComparer.OrdinalIgnoreCase;
      }
#if false
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
         if (ConvertVirtualPaths())
            url = VirtualPathUtility.ToAbsolute(url);
         if (!Contains(url))
            Add(CreateInjectorItem(url), order);

      }

/// <summary>
/// Indicates whether the virtual path notation "~/" should
/// be converted to an absolute path.
/// </summary>
/// <returns></returns>
      protected virtual bool ConvertVirtualPaths()
      {
         return true;
      }
#endif
/// <summary>
/// Creates the FileInjectorItem class used by the Add() method.
/// </summary>
/// <param name="url"></param>
/// <returns></returns>
      protected abstract BaseFileInjectorItem CreateInjectorItem(string url);
   }

}
