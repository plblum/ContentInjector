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
/// Extension methods designed for the InsertionsManager class that simplify accessing 
/// the Access&lt;T&gt;Add methods.
/// </summary>
   public static class InsertionsManagerExtensions
   {

      public static void AddScriptBlock(this InsertionsManager insertionManager, string script, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).Add(null, script, order);
      }
      public static void AddScriptFile(this InsertionsManager insertionManager, string url, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptFilesInserter>(group).Add(url, order);
      }
      public static void AddStyleFile(this InsertionsManager insertionManager, string url, int order = 0, string group = "")
      {
         insertionManager.Access<IStyleFilesInserter>(group).Add(url, order);
      }
      public static void AddMetaTag(this InsertionsManager insertionManager, string name, string content, int order = 0, string group = "")
      {
         insertionManager.Access<IMetaTagsInserter>(group).Add(name, content, order);
      }
      public static void AddMetaTag(this InsertionsManager insertionManager, MetaTagUsage usage, string name, string content, int order = 0, string group = "")
      {
         insertionManager.Access<IMetaTagsInserter>(group).Add(usage, name, content, order);
      }
      public static void AddHiddenField(this InsertionsManager insertionManager, string name, string value, int order = 0, string group = "")
      {
         insertionManager.Access<IHiddenFieldsInserter>(group).Add(name, value, order);
      }
      public static void AddPlaceholder(this InsertionsManager insertionManager, string content, int order = 0, string group = "")
      {
         insertionManager.Access<IPlaceholderInserter>(group).Add(content, order);
      }
      public static void AddTemplateBlock(this InsertionsManager insertionManager, string id, string content, int order = 0, string group = "")
      {
         insertionManager.Access<ITemplateBlocksInserter>(group).Add(id, content, order);
      }
      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, string value, bool htmlEncode = true, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, htmlEncode, order);
      }

      public static void ArrayDeclarationAsCode(this InsertionsManager insertionManager, string variableName, string value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclarationAsCode(variableName, value, order);
      }

      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, int value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, short value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, Int64 value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, double value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }

      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, decimal value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }


      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, Single value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }


      public static void ArrayDeclaration(this InsertionsManager insertionManager, string variableName, bool value, int order = 0, string group = "")
      {
         insertionManager.Access<IScriptBlocksInserter>(group).ArrayDeclaration(variableName, value, order);
      }


   }
}
