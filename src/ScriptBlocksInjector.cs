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
/// Creates a single script tag block with all scripts it contains in the user-specified order.
/// </summary>
/// <remarks>
/// <para>It can create a specialized script called an array declaration. This script defines
/// a new variable assigned to an array. Call the ArrayDeclaration methods to add
/// the variable and continue to append values to the array. Like all scripts in this class,
/// its order is determined by the order added or the order parameter.</para>
/// <para>It stores any class that implements IScriptBlockInjectorItem. So you can insert
/// other script generators into the same list output with the instance of the Injector.</para>
/// </remarks>
   public class ScriptBlocksInjector : BaseKeyedInjector<IScriptBlockInjectorItem>, IScriptBlocksInjector
   {
      protected override void PrefixContent(StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(StartScriptBlockTag);
      }

      protected override void PostfixContent(StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(EndScriptBlockTag);
      }

      public static string StartScriptBlockTag = "<script type=\"text/javascript\">";
      public static string EndScriptBlockTag = "</script>";

/// <summary>
/// When the item's Key property is unassigned, it will be assigned a unique value
/// because it means that the script should always be added.
/// </summary>
/// <param name="item"></param>
/// <param name="order"></param>
      public override void Add(IScriptBlockInjectorItem item, int order = 0)
      {
         if (String.IsNullOrEmpty(item.GetKey()))
           item.SetKey("UNQ" + (_uniqueIDCounter++).ToString());
         base.Add(item, order);
      }

      private int _uniqueIDCounter = 1;
   }
}
