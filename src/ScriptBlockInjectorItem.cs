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

namespace ContentInjector
{
/// <summary>
/// Data stored by the BaseScriptBlockInjector class to collect
/// URLs.
/// </summary>
   public class ScriptBlockInjectorItem : IScriptBlockInjectorItem
   {
      public ScriptBlockInjectorItem(string key, string script)
      {
         Key = key;
         Script = script;
      }

/// <summary>
/// Used to locate an existing item to determine if the script already exists.
/// </summary>
      public string Key { get; set; }

/// <summary>
/// The script of the item.
/// </summary>
      public string Script
      {
         get { return _script; }
         set
         {
            if (value.StartsWith("<"))
               throw new ArgumentException("Do not enclose scripts in script tags.");
            _script = value;
         }
      }
      private string _script;

      public virtual string GetScript()
      {
         return Script ?? String.Empty;
      }

      #region IKeyedInjectorItem Members

      string IKeyedInjectorItem.GetKey()
      {
         return Key;
      }

      void IKeyedInjectorItem.SetKey(string key)
      {
         Key = key;
      }

      #endregion
   }

   public interface IScriptBlockInjectorItem : IKeyedInjectorItem
   {
      string GetScript();
   }
}
