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
/// Extension methods designed for the ContentManager class that simplify accessing 
/// the Access&lt;T&gt;Add methods.
/// </summary>
   public static class ContentManagerExtensions
   {

      public static void ScriptBlock(this ContentManager contentInjector, string script, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).Add(null, script, order);
      }
      public static void ScriptFile(this ContentManager contentInjector, string url, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptFilesInjector>(group).Add(url, order);
      }
      public static void StyleFile(this ContentManager contentInjector, string url, int order = 0, string group = "")
      {
         contentInjector.Access<IStyleFilesInjector>(group).Add(url, order);
      }
      public static void MetaTag(this ContentManager contentInjector, string name, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IMetaTagsInjector>(group).Add(name, content, order);
      }
      public static void MetaTag(this ContentManager contentInjector, MetaTagUsage usage, string name, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IMetaTagsInjector>(group).Add(usage, name, content, order);
      }
      public static void HiddenField(this ContentManager contentInjector, string name, string value, int order = 0, string group = "")
      {
         contentInjector.Access<IHiddenFieldsInjector>(group).Add(name, value, order);
      }
      public static void Placeholder(this ContentManager contentInjector, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IPlaceholderInjector>(group).Add(content, order);
      }
      public static void TemplateBlock(this ContentManager contentInjector, string id, string content, int order = 0, string group = "")
      {
         contentInjector.Access<ITemplateBlocksInjector>(group).Add(id, content, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, string value, bool htmlEncode = true, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, htmlEncode, order);
      }

      public static void ArrayDeclarationAsCode(this ContentManager contentInjector, string variableName, string value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclarationAsCode(variableName, value, order);
      }

      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, int value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, short value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, Int64 value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, double value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }

      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, decimal value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }


      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, Single value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }


      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, bool value, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).ArrayDeclaration(variableName, value, order);
      }


   }
}
