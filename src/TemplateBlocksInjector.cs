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
/// For inserting script tags that are used for templating engines.
/// </summary>
/// <remarks>
/// <para>This library includes classes for various templating engines. None of them
/// are preregistered with the InjectorFactory. You must explicitly register
/// the desired class and associate it with ITemplateBlocksInjector.</para>
/// <para>
/// Here are the classes that support templates:
/// UnderscoreTemplateBlocksInjector, KnockoutTemplateBlocksInjector, 
/// jQueryTemplateBlocksInjector, and KendoUiTemplateBlocksInjector.</para>
/// </remarks>
   public class TemplateBlocksInjector : BaseKeyedInjector<BaseTemplateInjectorItem>, ITemplateBlocksInjector
   {
   }

}
