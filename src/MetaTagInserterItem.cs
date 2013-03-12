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
/// Data used by the MetaTagInserter to hold 
/// attribute values for a meta tag. By default, it creates
/// name and content attributes. If you can assign
/// </summary>
   public class MetaTagInserterItem : IKeyedInserterItem
   {
      public MetaTagInserterItem(string name, string content) : this(MetaTagUsage.Name, name, content)
      {
      }
      public MetaTagInserterItem(MetaTagUsage usage, string name, string content)
      {
         Name = name;
         Content = content ?? String.Empty;
         Usage = usage;
      }

/// <summary>
/// Defines the name of the attribute that shows the value of the Name property.
/// </summary>
      public MetaTagUsage Usage
      {
         get { return _usage; }
         set { _usage = value; }
      }
      private MetaTagUsage _usage;


/// <summary>
/// Assigned to the meta tag's [name], [charset], or [http-equiv] attribute,
/// depending on the Usage property.
/// </summary>
/// <remarks>
/// <para>This property is the key field and must be unique within the Insertion Point group.</para>
/// </remarks>
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
/// Assigned to the meta tag's [content] attribute.
/// </summary>
      public string Content { get; set; }

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

   public enum MetaTagUsage
   {
      Name,
      HttpEquiv,
      CharSet
   }
}
