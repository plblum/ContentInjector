﻿/* -----------------------------------------------------------
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

/// <summary>
/// Specifies a link tag to a style sheet file with its href
/// attribute pointing to that file.
/// </summary>
   public interface IStyleFilesInjector : IBaseTagsWithUrlInjector
   {
/// <summary>
/// Add a StyleFileInjectorItem with a specific order.
/// </summary>
/// <param name="order"></param>
/// <param name="item"></param>
      void Add(StyleFileInjectorItem item, int order = 0);
   }
}
