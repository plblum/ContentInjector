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

/// <summary>
/// Inserts meta tags with the name and description properties setup.
/// </summary>
   public interface IMetaTagsInjector : IInjector
   {

/// <summary>
/// Adds with the default order but does not add a duplicate Name (case insensitive match).
/// The Usage is Name.
/// </summary>
/// <param name="name"></param>
/// <param name="content"></param>
      void Add(string name, string content);


/// <summary>
/// Adds but does not add a duplicate Name (case insensitive match).
/// The Usage is Name.
/// </summary>
/// <param name="name"></param>
/// <param name="content"></param>
/// <param name="order"></param>
      void Add(string name, string content, int order);

/// <summary>
/// Adds but does not add a duplicate Name (case insensitive match).
/// Allows changing the usage.
/// </summary>
/// <param name="usage"></param>
/// <param name="name"></param>
/// <param name="content"></param>
/// <param name="order"></param>
      void Add(MetaTagUsage usage, string name, string content, int order);
   }
}
