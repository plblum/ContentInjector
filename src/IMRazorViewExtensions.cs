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
using System.Web.Mvc;

namespace InsertionsManagement
{
   public static class IMRazorViewExtensions
   {
      public static InsertionsManager InsertionsManager(this WebViewPage webPage)
      {
         InsertionsManager im = webPage.ViewData["InsertionsManager"] as InsertionsManager;
         if (im == null)
            throw new NotSupportedException("The InsertionsManager has not been added to the page. Ensure that the custom RazorView which creates the InsertionManager is being used. Check Application_Start to ensure the default RazorViewEngine was removed [ViewEngines.Engines.Clear()] and the custom view was added [ViewEngines.Engines.Add(new InsertionsManagement.IMRazorViewEngine())].");
         return im;
      }
   }
}
