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
         contentInjector.Access<IScriptBlocksInjector>(group).Add(new ScriptBlockInjectorItem(null, script), order);
      }
      public static void ScriptBlock(this ContentManager contentInjector, string key, string script, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).Add(new ScriptBlockInjectorItem(key, script), order);
      }
      public static void ScriptFile(this ContentManager contentInjector, string url, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptFilesInjector>(group).Add(new ScriptFileInjectorItem(url), order);
      }
      public static void StyleFile(this ContentManager contentInjector, string url, int order = 0, string group = "")
      {
         contentInjector.Access<IStyleFilesInjector>(group).Add(new StyleFileInjectorItem(url), order);
      }
      public static void MetaTag(this ContentManager contentInjector, string name, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IMetaTagsInjector>(group).Add(new MetaTagInjectorItem(name, content), order);
      }
      public static void MetaTag(this ContentManager contentInjector, MetaTagUsage usage, string name, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IMetaTagsInjector>(group).Add(new MetaTagInjectorItem(usage, name, content), order);
      }
      public static void HiddenField(this ContentManager contentInjector, string name, string value, int order = 0, string group = "")
      {
         contentInjector.Access<IHiddenFieldsInjector>(group).Add(new HiddenFieldInjectorItem(name, value), order);
      }
      public static void Placeholder(this ContentManager contentInjector, string content, int order = 0, string group = "")
      {
         contentInjector.Access<IPlaceholderInjector>(group).Add(new PlaceholderInjectorItem(content), order);
      }

/// <summary>
/// Creates a BaseTemplateInjectorItem subclass based on the ContentManagerExtensions.DefaultTemplateType.
/// If you don't set it in Application_Start, it will use the KnockoutTemplateInjectorItem.
/// </summary>
/// <param name="contentInjector"></param>
/// <param name="id"></param>
/// <param name="content"></param>
/// <param name="order"></param>
/// <param name="group"></param>
      public static void TemplateBlock(this ContentManager contentInjector, string id, string content, int order = 0, string group = "")
      {
         BaseTemplateInjectorItem item = (BaseTemplateInjectorItem)Activator.CreateInstance(DefaultTemplateType, id, content);
         contentInjector.Access<ITemplateBlocksInjector>(group).Add(item, order);
      }

/// <summary>
/// Defines the type of TemplateInjectorItem that is created by the Template extension method.
/// It must have a constructor that takes two parameters: id and content (both strings).
/// </summary>
      public static Type DefaultTemplateType = typeof(KnockoutTemplateInjectorItem);

      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, string value, bool htmlEncode = true, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value, htmlEncode);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }

      public static void ArrayDeclarationAsCode(this ContentManager contentInjector, string variableName, string value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }

      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, int value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, short value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, Int64 value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }
      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, double value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }

      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, decimal value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }


      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, Single value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }


      public static void ArrayDeclaration(this ContentManager contentInjector, string variableName, bool value, int order = 0, string group = "")
      {
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(variableName);
         item.Add(value);
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }


// using InjectorItem classes
      public static void ScriptBlock(this ContentManager contentInjector, ScriptBlockInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }
      public static void ScriptFile(this ContentManager contentInjector, ScriptFileInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptFilesInjector>(group).Add(item, order);
      }
      public static void StyleFile(this ContentManager contentInjector, StyleFileInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IStyleFilesInjector>(group).Add(item, order);
      }
      public static void MetaTag(this ContentManager contentInjector, MetaTagInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IMetaTagsInjector>(group).Add(item, order);
      }
      public static void HiddenField(this ContentManager contentInjector, HiddenFieldInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IHiddenFieldsInjector>(group).Add(item, order);
      }
      public static void Placeholder(this ContentManager contentInjector, PlaceholderInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IPlaceholderInjector>(group).Add(item, order);
      }

      public static void TemplateBlock(this ContentManager contentInjector, BaseTemplateInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<ITemplateBlocksInjector>(group).Add(item, order);
      }

      public static void ArrayDeclaration(this ContentManager contentInjector, ArrayDeclarationInjectorItem item, int order = 0, string group = "")
      {
         contentInjector.Access<IScriptBlocksInjector>(group).Add(item, order);
      }


   }
}
