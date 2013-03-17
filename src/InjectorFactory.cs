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
using System.ComponentModel.Design;

namespace ContentInjector
{
   public class InjectorFactory
   {
      public InjectorFactory()
      {
         // creates the default services 
         AddDefaultServices();
      }

/// <summary>
/// The Key is an interface type that implements IInjector. The Value must be
/// a class Type that implements the same interface type specified in the Key.
/// New instances will be created with each call to Get().
/// It's parameterless constructor is always called.
/// If you need to populate it with specific property values, create a subclass
/// that assigns those properties in the constructor.
/// </summary>
      protected Dictionary<Type, Type> _injectors = new Dictionary<Type, Type>();

/// <summary>
/// Adds or replaces an Injector based on the Interface type
/// </summary>
/// <remarks>
/// <para>The parameterless constructor of the injecterClassType is always used to create it.
/// If this class needs additional properties assigned, subclass the injecterClassType
/// and assign those properties in the constructor. Then use that subclass here.</para>
/// <para>This does not support multithreading. If called on the global default,
/// only do so in Application Startup.</para>
/// </remarks>
/// <param name="interfaceType">The unique Interface type to associate with the injecter.</param>
/// <param name="injecterClassType">The class that implements the interface type.
/// It will be created using the parameterless constructor in the Get() method.</param>
      public void Register(Type interfaceType, Type injecterClassType)
      {
         if ((injecterClassType == null) || (interfaceType == null))
            throw new ArgumentNullException();
         if (!typeof(IInjector).IsAssignableFrom(interfaceType))
            throw new ArgumentException("Interface Type must derive from IInjector.");
         if (_injectors.ContainsKey(interfaceType))
            _injectors.Remove(interfaceType);
         _injectors.Add(interfaceType, injecterClassType);
      }

/// <summary>
/// Note: The ITemplateBlocksInjector is not added. The user must explicitly
/// register the desired implementation of ITemplateBlocksInjector based on 
/// the template engine they use.
/// </summary>
      protected virtual void AddDefaultServices()
      {
         _injectors.Add(typeof(IScriptFilesInjector), typeof(ScriptFilesInjector));
         _injectors.Add(typeof(IStyleFilesInjector), typeof(StyleFilesInjector));
         _injectors.Add(typeof(IMetaTagsInjector), typeof(MetaTagsInjector));
         _injectors.Add(typeof(IScriptBlocksInjector), typeof(ScriptBlocksInjector));
         _injectors.Add(typeof(IHiddenFieldsInjector), typeof(HiddenFieldsInjector));
         _injectors.Add(typeof(IPlaceholderInjector), typeof(PlaceholderInjector));
      }

/// <summary>
/// Returns a unique instance of the class associated with interfaceType.
/// </summary>
/// <param name="interfaceType">The unique Interface type to associate with the injecter.</param>
/// <param name="throwException">When true, throw an exception if the interface type is not registered.
/// If not supplied, it uses false.</param>
/// <returns>The instance or null if not found (and throwException is false).</returns>
      public virtual IInjector Create(Type interfaceType, bool throwException = false)
      {
         if (interfaceType == null)
            throw new ArgumentNullException();
         Type injecterClassType = null;
         if (_injectors.TryGetValue(interfaceType, out injecterClassType))
            return (IInjector) Activator.CreateInstance(injecterClassType);
         if (throwException)
            throw new NotSupportedException(String.Format("The InjectorFactory does not recognize the interface type {0}", interfaceType.FullName));
         return null;
      }

/// <summary>
/// A global factory that is used when ContentInjector is not passed an instance of
/// InjectorFactory in its constructor.
/// </summary>
      public static InjectorFactory Default
      {
         get
         {
            if (_default == null)
            {
         // multi threading note: 
         // It creates the same data each time. If two threads enter this code,
         // they may use different instances, but the operation is the same.
         // Once _default is assigned, later threads will all share a common instance.
               _default = new InjectorFactory();   // establishes default services
            }
            return _default;
         }
      }
      private static InjectorFactory _default;
   }
}
