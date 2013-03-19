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
/// Data stored by the BaseTagWithUrlInjector class to collect
/// URLs.
/// </summary>
   public abstract class BaseFileInjectorItem : IKeyedInjectorItem
   {
      public BaseFileInjectorItem(string fileID)
      {
         FileID = fileID;
      }

/// <summary>
/// Identifies the file. This class treats it as URL, but it subclasses may use it for other 
/// identities, such as a bundle or group name that will be resolved into one or more tags.
/// </summary>
      public string FileID
      {
         get { return _fileID ?? String.Empty; }
         set
         {
            _fileID = value;
            if (ConvertVirtualPaths())
               _fileID = VirtualPathUtility.ToAbsolute(_fileID);
         }
      }
      private string _fileID;


      #region IKeyedInjectorItem Members

      string IKeyedInjectorItem.GetKey()
      {
         return FileID;
      }

      void IKeyedInjectorItem.SetKey(string key)
      {
         FileID = key ?? String.Empty;
      }

/// <summary>
/// Existing item is unchanged.
/// </summary>
/// <param name="item"></param>
/// <returns>False</returns>
      public virtual bool Merge(IKeyedInjectorItem item)
      {
         return false;
      }


      #endregion

/// <summary>
/// Indicates whether the virtual path notation "~/" should
/// be converted to an absolute path.
/// </summary>
/// <returns></returns>
      protected virtual bool ConvertVirtualPaths()
      {
         return true;
      }

/// <summary>
/// Creates the tag(s) based on the data of this object.
/// </summary>
/// <param name="httpContext"></param>
/// <returns></returns>
      public abstract string GetContent(HttpContextBase httpContext);
   }



}
