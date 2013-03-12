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

namespace InsertionsManagement
{

/// <summary>
/// Collects javascript blocks, without any enclosing script tags,
/// and outputs them in a specific order. 
/// </summary>
/// <remarks>
/// <para>Script blocks positions are important. You often have two of these on the page.
/// One above HTML and the other below. Typically Insertion Points are defined with group
/// names to define each of these positions.</para>
/// <para>It can create a specialized script called an array declaration. This script defines
/// a new variable assigned to an array. Call the ArrayDeclaration methods to add
/// the variable and continue to append values to the array. Like all scripts in this class,
/// its order is determined by the order added or the order parameter.</para>
/// <para>Other interfaces can handle specific blocks, like IJQueryValidateRulesBlockInserter,
/// to collect all jquery validation rules. These are left to the user.</para>
/// </remarks>
   public interface IScriptBlocksInserter : IInserter
   {

/// <summary>
/// Adds with the default order
/// </summary>
/// <param name="key">Used to find an existing item with the Contains method or replace an existing item.
/// If null or "", a unique value is internally generated.</param>
/// <param name="script"></param>
      void Add(string key, string script);


/// <summary>
/// Adds in the specified order.
/// </summary>
/// <param name="key">Used to find an existing item with the Contains method or replace an existing item.
/// If null or "", a unique value is internally generated.</param>
/// <param name="script"></param>
/// <param name="order"></param>
      void Add(string key, string script, int order);

/// <summary>
/// For creating a script that is an array declaration.
/// Adds a string value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, string value, bool htmlEncode = false, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a boolean value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, bool value, int order = 0);



/// <summary>
/// For creating a script that is an array declaration.
/// Adds an integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, int value, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a 16 bit integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, short value, int order = 0);



/// <summary>
/// For creating a script that is an array declaration.
/// Adds a 64 bit integer value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, Int64 value, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a double value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, double value, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a Single value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, Single value, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a decimal value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclaration(string variableName, decimal value, int order = 0);


/// <summary>
/// For creating a script that is an array declaration.
/// Adds a block of javascript code as the value to the end of the array associated with variableName.
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="order"></param>
      void ArrayDeclarationAsCode(string variableName, string value, int order = 0);

   }

}
