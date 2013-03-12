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
using System.ComponentModel.Design;

namespace InsertionsManagement
{
   public class InserterFactory
   {
      public InserterFactory()
      {
         // creates the default services 
         AddDefaultServices();
      }

/// <summary>
/// The Key is an interface type that implements IInserter. The Value must be
/// a class Type that implements the same interface type specified in the Key.
/// New instances will be created with each call to Get().
/// It's parameterless constructor is always called.
/// If you need to populate it with specific property values, create a subclass
/// that assigns those properties in the constructor.
/// </summary>
      protected Dictionary<Type, Type> _inserters = new Dictionary<Type, Type>();

/// <summary>
/// Adds or replaces an Inserter based on the Interface type
/// </summary>
/// <remarks>
/// <para>The parameterless constructor of the inserterClassType is always used to create it.
/// If this class needs additional properties assigned, subclass the inserterClassType
/// and assign those properties in the constructor. Then use that subclass here.</para>
/// <para>This does not support multithreading. If called on the global default,
/// only do so in Application Startup.</para>
/// </remarks>
/// <param name="interfaceType">The unique Interface type to associate with the inserter.</param>
/// <param name="inserterClassType">The class that implements the interface type.
/// It will be created using the parameterless constructor in the Get() method.</param>
      public void Register(Type interfaceType, Type inserterClassType)
      {
         if ((inserterClassType == null) || (interfaceType == null))
            throw new ArgumentNullException();
         if (!typeof(IInserter).IsAssignableFrom(interfaceType))
            throw new ArgumentException("Interface Type must derive from IInserter.");
         if (_inserters.ContainsKey(interfaceType))
            _inserters.Remove(interfaceType);
         _inserters.Add(interfaceType, inserterClassType);
      }

/// <summary>
/// Note: The ITemplateBlocksInserter is not added. The user must explicitly
/// register the desired implementation of ITemplateBlocksInserter based on 
/// the template engine they use.
/// </summary>
      protected virtual void AddDefaultServices()
      {
         _inserters.Add(typeof(IScriptFilesInserter), typeof(ScriptFilesInserter));
         _inserters.Add(typeof(IStyleFilesInserter), typeof(StyleFilesInserter));
         _inserters.Add(typeof(IMetaTagsInserter), typeof(MetaTagsInserter));
         _inserters.Add(typeof(IScriptBlocksInserter), typeof(ScriptBlocksInserter));
         _inserters.Add(typeof(IHiddenFieldsInserter), typeof(HiddenFieldsInserter));
         _inserters.Add(typeof(IPlaceholderInserter), typeof(PlaceholderInserter));
      }

/// <summary>
/// Returns a unique instance of the class associated with interfaceType.
/// </summary>
/// <param name="interfaceType">The unique Interface type to associate with the inserter.</param>
/// <param name="throwException">When true, throw an exception if the interface type is not registered.
/// If not supplied, it uses false.</param>
/// <returns>The instance or null if not found (and throwException is false).</returns>
      public virtual IInserter Create(Type interfaceType, bool throwException = false)
      {
         if (interfaceType == null)
            throw new ArgumentNullException();
         Type inserterClassType = null;
         if (_inserters.TryGetValue(interfaceType, out inserterClassType))
            return (IInserter) Activator.CreateInstance(inserterClassType);
         if (throwException)
            throw new NotSupportedException(String.Format("The InserterFactory does not recognize the interface type {0}", interfaceType.FullName));
         return null;
      }

/// <summary>
/// A global factory that is used when InsertionManager is not passed an instance of
/// InserterFactory in its constructor.
/// </summary>
      public static InserterFactory Default
      {
         get
         {
            if (_default == null)
            {
         // multi threading note: 
         // It creates the same data each time. If two threads enter this code,
         // they may use different instances, but the operation is the same.
         // Once _default is assigned, later threads will all share a common instance.
               _default = new InserterFactory();   // establishes default services
            }
            return _default;
         }
      }
      private static InserterFactory _default;
   }
}
