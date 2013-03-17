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

namespace ContentInjector
{
   /// <summary>
   /// Register this in Application_Start like this: ViewEngines.Engines.Clear();ViewEngines.Engines.Add(new IMRazorViewEngine());
   /// </summary>
   public class CIRazorViewEngine : RazorViewEngine
   {
      public CIRazorViewEngine()
         : base()
      {
      }

      public CIRazorViewEngine(IViewPageActivator viewPageActivator)
         : base(viewPageActivator)
      {
      }

      protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
      {
         return new CMRazorView(controllerContext, partialPath,
                     layoutPath: null, runViewStartPages: false, 
                     viewStartFileExtensions: FileExtensions, viewPageActivator: ViewPageActivator);
      }

      protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
      {
         var view = new CMRazorView(controllerContext, viewPath,
                     layoutPath: masterPath, runViewStartPages: true, 
                     viewStartFileExtensions: FileExtensions, viewPageActivator: ViewPageActivator);
         return view;
      }

   }
}
