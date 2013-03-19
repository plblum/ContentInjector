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
   public abstract class BaseTemplateInjectorItem : IKeyedInjectorItem
   {
      public BaseTemplateInjectorItem(string id, string content)
      {
         ID = id;
         Content = content ?? String.Empty;
      }

/// <summary>
/// Assigns the ID attribute on the script tag.
/// </summary>
      public string ID
      {
         get { return _id; }
         set
         {
            if (String.IsNullOrEmpty(value))
               throw new ArgumentException("Must assign text.");
            _id = value;
         }
      }
      private string _id;

/// <summary>
/// The markup and template notations that appears within a script tag with type='text/html'.
/// </summary>
      public string Content { get; set; }

      #region IKeyedInjectorItem Members

      public string GetKey()
      {
         return ID;
      }

      public void SetKey(string key)
      {
         ID = key;
      }

/// <summary>
/// Existing item is unchanged.
/// </summary>
/// <param name="item"></param>
/// <returns>False</returns>
      public virtual bool Merge(IKeyedInjectorItem item)
      {
         return false;
      }

      #endregion

      public virtual string GetContent(HttpContextBase context)
      {
         return String.Format(DefaultTemplateTagFormat, ID, Content, GetTemplateType());
      }


/// <summary>
/// Used by the script tag's type attribute. For example, "text/template".
/// works with underscore.js and backbone.js. Jquery-templates uses "x-jquery-tmpl".
/// Knockout.js uses "text/html". KendoUI uses "text/x-kendo-template".
/// </summary>
      protected abstract string GetTemplateType();

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the ID is inserted and {1} where Content is inserted.
/// {2} is the value for the type attribute and retrieved from the GetTemplateType() method.
/// </summary>
      public static string DefaultTemplateTagFormat = "<script id=\"{0}\" type=\"{2}\" >\r\n{1}\r\n</script>";


   }
/// <summary>
/// For template engines like Underscore.js and Backbone.js which use
/// the type attribute value "text/template" in the script tag.
/// </summary>
   public class UnderscoreTemplateInjectorItem : BaseTemplateInjectorItem
   {
      public UnderscoreTemplateInjectorItem(string id, string content)
         : base(id, content)
      {
      }
      protected override string GetTemplateType()
      {
         return "text/template";
      }
   }

/// <summary>
/// For template engines like Knockout.js and John Resig's templating engine which use
/// the type attribute value "text/html" in the script tag.
/// </summary>
   public class KnockoutTemplateInjectorItem : BaseTemplateInjectorItem
   {
      public KnockoutTemplateInjectorItem(string id, string content)
         : base(id, content)
      {
      }
      protected override string GetTemplateType()
      {
         return "text/html";
      }
   }

/// <summary>
/// For the Telerik KendoUI template engine.
/// </summary>
   public class KendoUiTemplateInjectorItem : BaseTemplateInjectorItem
   {
      public KendoUiTemplateInjectorItem(string id, string content)
         : base(id, content)
      {
      }

      protected override string GetTemplateType()
      {
         return "text/x-kendo-template";
      }
   }

/// <summary>
/// For the jquery template engine.
/// </summary>
   public class jQueryTemplateInjectorItem : BaseTemplateInjectorItem
   {
      public jQueryTemplateInjectorItem(string id, string content)
         : base(id, content)
      {
      }

      protected override string GetTemplateType()
      {
         return "text/x-jquery-tmpl";
      }
   }
}
