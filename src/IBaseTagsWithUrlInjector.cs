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
   public interface IBaseTagsWithUrlInjector : IInjector
   {
/// <summary>
/// Adds with the default order but does not add a duplicate URL (case insensitive match).
/// </summary>
/// <param name="url"></param>
      void Add(string url);


/// <summary>
/// Adds but does not add a duplicate URL (case insensitive match).
/// </summary>
/// <param name="url"></param>
/// <param name="order"></param>
      void Add(string url, int order);
   }
}
