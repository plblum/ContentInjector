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
/// Creates a single script tag block with all scripts it contains in the user-specified order.
/// </summary>
/// <remarks>
/// <para>It can create a specialized script called an array declaration. This script defines
/// a new variable assigned to an array. Call the ArrayDeclaration methods to add
/// the variable and continue to append values to the array. Like all scripts in this class,
/// its order is determined by the order added or the order parameter.</para>
/// <para>It stores any class that implements IScriptBlockInserterItem. So you can insert
/// other script generators into the same list output with the instance of the Inserter.</para>
/// </remarks>
   public class ScriptBlocksInserter : BaseKeyedInserter<IScriptBlockInserterItem>, IScriptBlocksInserter
   {
#if false
/// <param name="httpContext"></param>
/// <returns>If there are no scripts, the empty string is returned.</returns>
      public override string GetContent(HttpContextBase httpContext)
      {
         if (_orderedList.Count == 0)
            return String.Empty;

         StringBuilder sb = new StringBuilder();

         sb.AppendLine(StartScriptBlockTag);
         foreach (var orders in _orderedList)
         {
            foreach (IScriptBlockInserterItem items in orders.Value)
            {
               sb.AppendLine(items.GetScript());
            }
         }

         sb.AppendLine(EndScriptBlockTag);

         return sb.ToString();
      }
#endif
      protected override void PrefixContent(StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(StartScriptBlockTag);
      }

      protected override void ItemContent(IScriptBlockInserterItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(item.GetScript());
      }

      protected override void PostfixContent(StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(EndScriptBlockTag);
      }

      public static string StartScriptBlockTag = "<script type=\"text/javascript\">";
      public static string EndScriptBlockTag = "</script>";


/// <summary>
/// Adds with the default order
/// </summary>
/// <param name="key">Used to find an existing item with the Contains method or replace an existing item.
/// If null or "", a unique value is internally generated.</param>
/// <param name="script"></param>
      public void Add(string key, string script)
      {
         Add(key, script, 0);
      }


/// <summary>
/// Adds in the specified order.
/// </summary>
/// <param name="key">Used to find an existing item with the Contains method or replace an existing item.
/// If null or "", a unique value is internally generated.</param>
/// <param name="script"></param>
/// <param name="order"></param>
      public virtual void Add(string key, string script, int order)
      {
         if (String.IsNullOrEmpty(key))
            key = (_uniqueIDCounter++).ToString();
         else
         {
            IScriptBlockInserterItem existingItem = null;
            if (_sortedByKey.TryGetValue(key, out existingItem))
            {
               if (existingItem is ScriptBlockInserterItem)
                  // NOTE: order is ignored in this case.
                  ((ScriptBlockInserterItem)existingItem).Script = script;
               else
                  throw new InvalidCastException("");
               return;
            }
         }

         Add(new ScriptBlockInserterItem(key, script), order);

      }

      private int _uniqueIDCounter = 1;


/// <summary>
/// Returns an instance of ArrayDeclarationInserterItem for the variable name,
/// adding it if not declared.
/// </summary>
/// <param name="variableName"></param>
/// <param name="order"></param>
/// <returns></returns>
      protected ArrayDeclarationInserterItem GetDeclaration(string variableName, int order)
      {
         IScriptBlockInserterItem item;
         if (!this._sortedByKey.TryGetValue(variableName, out item))
         {
            item = new ArrayDeclarationInserterItem(variableName);
            base.Add(item, order);
         }
         return (ArrayDeclarationInserterItem) item;
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a string value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, string value, bool htmlEncode = false, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value, htmlEncode);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a boolean value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, bool value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds an integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, int value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a 16 bit integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, short value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a 64 bit integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, Int64 value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a double value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, double value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a Single value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, Single value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }

/// <summary>
/// For creating a script that is an array declaration.
/// Adds a decimal value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclaration(string variableName, decimal value, int order = 0)
      {
         GetDeclaration(variableName, order).Add(value);
      }


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a block of javascript code as the value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      public void ArrayDeclarationAsCode(string variableName, string value, int order = 0)
      {
         GetDeclaration(variableName, order).AddCode(value);
      }

   }
}
