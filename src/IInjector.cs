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
/// Classes supported by ContentInjector must implement this.
/// Its GetContent() method returns the string that will replace
/// a placeholder tag in the HTML markup.
/// </summary>
/// <remarks>
/// <para>Each class that implements this interface should define its own interface
/// based on IInjector. The interface is used to request the class through a
/// the InsertionsServices class. It allows switching implementations as needed.</para>
/// <para>The consumer of ContentInjector passes the appropriate interface
/// to ContentInjector.Access[T] where T is the interface.</para>
/// </remarks>
   public interface IInjector
   {
/// <summary>
/// This string is used to replace the entire
/// placeholder tag.
/// </summary>
/// <returns></returns>
      string GetContent(HttpContextBase httpContext);
   }
}
