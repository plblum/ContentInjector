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
/// Use with ScriptFilesInjector to host an item.
/// </summary>
   public class ScriptFileInjectorItem : BaseFileInjectorItem
   {
      public ScriptFileInjectorItem(string url)
         : base(url)
      {
      }

      public override string GetContent(HttpContextBase httpContext)
      {
         return String.Format(DefaultScriptFileTagFormat,  HttpUtility.UrlPathEncode(FileID)); // NOTE: url is already converted from virtual to absolute path
      }

/// <summary>
/// The pattern used to create the tag. It is a global in case the user prefers a different pattern.
/// Always use {0} to indicate where the URL is inserted.
/// </summary>
      public static string DefaultScriptFileTagFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>";

   }


}
