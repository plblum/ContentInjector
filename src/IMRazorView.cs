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
using System.Web;
using System.Web.Mvc;


namespace InsertionsManagement
{

/// <summary>
/// Extends the RazorView class to use the InsertionsManager.
/// </summary>
/// <remarks>
/// <para>If you have your own RazorView subclass, enhance your RenderView() method similarly to the
/// one used here.</para>
/// </remarks>
   public class IMRazorView : RazorView
   {

        public IMRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions)
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions) 
        {
        }

        public IMRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator) 
        {
        }


      protected override void RenderView(ViewContext viewContext, System.IO.TextWriter writer, object instance)
      {
         using (var insertionsManager = new InsertionsManager(writer, viewContext.HttpContext, InserterFactory.Default))
         {
            viewContext.ViewData["InsertionsManager"] = insertionsManager;
            base.RenderView(viewContext, insertionsManager.ContentWriter, instance);

            insertionsManager.UpdatePage();
         }
      }
   }
}
