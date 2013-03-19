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
/// Creates a single HTML tag that hosts a URL, such as a script or link tag.
/// </summary>
/// <remarks>
/// </remarks>
   public class MetaTagsInjector : BaseKeyedInjector<MetaTagInjectorItem>, IMetaTagsInjector
   {

      protected override IComparer<string> GetComparer()
      {
         return StringComparer.OrdinalIgnoreCase;
      }


   }

}
