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
/// For inserting script tags that are used for templating engines.
/// </summary>
/// <remarks>
/// <para>This library includes classes for various templating engines. None of them
/// are preregistered with the InjectorFactory. You must explicitly register
/// the desired class and associate it with ITemplateBlocksInjector.</para>
/// <para>
/// Here are the classes that support templates:
/// UnderscoreTemplateBlocksInjector, KnockoutTemplateBlocksInjector, 
/// jQueryTemplateBlocksInjector, and KendoUiTemplateBlocksInjector.</para>
/// </remarks>
   public abstract class BaseTemplateBlocksInjector : BaseKeyedInjector<TemplateInjectorItem>, ITemplateBlocksInjector
   {
#if false
      #region IInjector Members

      public override string GetContent(HttpContextBase httpContext)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var orders in _orderedList)
         {
            foreach (TemplateInjectorItem item in orders.Value)
            {
               sb.AppendLine(GetTag(item, httpContext));
            }
         }

         return sb.ToString();
      }
      #endregion
#endif
      protected override void ItemContent(TemplateInjectorItem item, StringBuilder sb, HttpContextBase httpContext)
      {
         sb.AppendLine(String.Format(TemplateTagPattern, item.ID, item.Content, GetTemplateType()));
      }

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the ID is inserted and {1} where Content is inserted.
/// {2} is the value for the type attribute and retrieved from the GetTemplateType() method.
/// </summary>
      public static string TemplateTagPattern = "<script id=\"{0}\" type=\"{2}\" >\r\n{1}\r\n</script>";

/// <summary>
/// Used by the script tag's type attribute. For example, "text/template".
/// works with underscore.js and backbone.js. Jquery-templates uses "x-jquery-tmpl".
/// Knockout.js uses "text/html". KendoUI uses "text/x-kendo-template".
/// </summary>
      protected abstract string GetTemplateType();


/// <summary>
/// Adds with the default order.
/// </summary>
/// <param name="id"></param>
      public void Add(string id, string content)
      {
         Add(id, content, 0);
      }


/// <summary>
/// Adds but does not add a duplicate ID (case insensitive match).
/// </summary>
/// <param name="url"></param>
/// <param name="order"></param>
      public virtual void Add(string id, string content, int order)
      {
         if (!Contains(id))
            Add(new TemplateInjectorItem(id, content), order);

      }
   }

/// <summary>
/// For template engines like Underscore.js and Backbone.js which use
/// the type attribute value "text/template" in the script tag.
/// </summary>
   public class UnderscoreTemplateBlocksInjector : BaseTemplateBlocksInjector
   {
      protected override string GetTemplateType()
      {
         return "text/template";
      }
   }

/// <summary>
/// For template engines like Knockout.js and John Resig's templating engine which use
/// the type attribute value "text/html" in the script tag.
/// </summary>
   public class KnockoutTemplateBlocksInjector : BaseTemplateBlocksInjector
   {
      protected override string GetTemplateType()
      {
         return "text/html";
      }
   }

/// <summary>
/// For the Telerik KendoUI template engine.
/// </summary>
   public class KendoUiTemplateBlocksInjector : BaseTemplateBlocksInjector
   {
      protected override string GetTemplateType()
      {
         return "text/x-kendo-template";
      }
   }

/// <summary>
/// For the jquery template engine.
/// </summary>
   public class jQueryTemplateBlocksInjector : BaseTemplateBlocksInjector
   {
      protected override string GetTemplateType()
      {
         return "text/x-jquery-tmpl";
      }
   }

}
