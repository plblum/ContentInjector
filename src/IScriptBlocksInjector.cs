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
/// Collects javascript blocks, without any enclosing script tags,
/// and outputs them in a specific order. 
/// </summary>
/// <remarks>
/// <para>Script blocks positions are important. You often have two of these on the page.
/// One above HTML and the other below. Typically Injection Points are defined with group
/// names to define each of these positions.</para>
/// <para>It can create a specialized script called an array declaration by 
/// adding an ArrayDeclarationInjectorItem object. This script defines
/// a new variable assigned to an array. Call the ArrayDeclaration methods to add
/// the variable and continue to append values to the array. Like all scripts in this class,
/// its order is determined by the order added or the order parameter.</para>
/// <para>Other interfaces can handle specific blocks, like IJQueryValidateRulesBlockInjector,
/// to collect all jquery validation rules. These are left to the user.</para>
/// </remarks>
   public interface IScriptBlocksInjector : IInjector
   {
      void Add(IScriptBlockInjectorItem item, int order = 0);

   }

}
