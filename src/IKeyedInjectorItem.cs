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
/// Companion to BaseKeyedInjector to describe the objects it collects.
/// </summary>
/// <remarks>
/// <para>The BaseKeyedInjector operates on the "Key" value as identity.
/// But individual usages of BaseKeyedInjector can declare more suitable names
/// for the Key as a property like Name and Content. This interface
/// exposes that more suitable named property.</para>
/// </remarks>
   public interface IKeyedInjectorItem
   {
      string GetKey();
      void SetKey(string key);

/// <summary>
/// When adding a new item with the same key, call this on the existing
/// item, passing in the new item. It will determine how to use the new item,
/// either by updating itself or ignoring the item. In either case,
/// the new item will be discarded and should not be added to the _orderedList
/// or _sortedByKey collections of the caller.
/// </summary>
/// <param name="item"></param>
/// <returns>When true, the item was updated. When false, the item was ignored.</returns>
      bool Merge(IKeyedInjectorItem item);

/// <summary>
/// Converts the data into the string that will be output by the Inserter.
/// </summary>
/// <returns></returns>
      string GetContent(HttpContextBase httpContext);
   }
}
