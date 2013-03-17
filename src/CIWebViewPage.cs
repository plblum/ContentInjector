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
using System.Web.Mvc;
using ContentInjector;

namespace ContentInjector
{
   public abstract class CIWebViewPage : WebViewPage
   {
      public CIWebViewPage()
         : base()
      {
      }

/// <summary>
/// Exposes the ContentManager class so users can call its methods to add their content.
/// For example, @Injector.ScriptFile("url") (when using the ContentManagerExtensions.ScriptFile
/// extension method.)
/// </summary>
      public virtual ContentManager Injector
      {
         get
         {
            if (_injector == null)
            {
               _injector = CIRazorViewExtensions.ContentManager(this);
            }
            return _injector;
         }
      }
      private ContentManager _injector;
   }

   public abstract class CIWebViewPage<TModel> : WebViewPage<TModel>
   {
      public CIWebViewPage()
         : base()
      {
      }

/// <summary>
/// Exposes the ContentManager class so users can call its methods to add their content.
/// For example, @Injector.ScriptFile("url") (when using the ContentManagerExtensions.ScriptFile
/// extension method.)
/// </summary>
      public virtual ContentManager Injector
      {
         get
         {
            if (_injector == null)
            {
               _injector = CIRazorViewExtensions.ContentManager(this);
            }
            return _injector;
         }
      }
      private ContentManager _injector;
   }

}
