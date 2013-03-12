﻿/* -----------------------------------------------------------
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
using System.Collections;
using System.Web;

namespace InsertionsManagement
{

/// <summary>
/// An IInserter that maintains multiple entries in a specific order. It stores
/// elements that implement IKeyedInserterItem.
/// </summary>
/// <remarks>
/// <para>There are two ordering factors: an order number and a position in a list based on the order number.</para>
/// <para>The order number owns a list of items. Lower order numbers are earlier in the overall list
/// of object. Once an order number is used, items are appended to the list in the order they are added.</para>
/// </remarks>
   public abstract class BaseKeyedInserter<T> : IInserter, IDisposable
      where T : IKeyedInserterItem
   {
      public BaseKeyedInserter()
      {
         _orderedList = new SortedList<int, IList>();

         _sortedByKey = new SortedList<string, T>(this.GetComparer());
      }

/// <summary>
/// The ordered BaseInserterItems where the Key is the order number
/// and the value is a list of BaseInserterItems associated with that order number.
/// </summary>
      protected SortedList<int, IList> _orderedList;

/// <summary>
/// Holds the same data as in _orderedList, but sorted by IKeyedInserterItem.GetKey().
/// It initially sorts by case sensitive match. If you want to use case insensitive, override GetComparer
/// to return StringComparer.OrdinalIgnoreCase.
/// This list will not host duplicates of the Content string. It is used to determine
/// if the content was already added in subclasses.
/// </summary>
      protected SortedList<string, T> _sortedByKey;

/// <summary>
/// When adding an IKeyedInserterItem, it is checked for IDisposable.
/// If found this is true and the Dispose() method will call its Dispose() method.
/// </summary>
      private bool _itemImplementsIDisposable;

/// <summary>
/// For sorting the key in the _sortedByContent list.
/// </summary>
/// <returns>StringComparer.Ordinal</returns>
      protected virtual IComparer<string> GetComparer()
      {
         return StringComparer.Ordinal;
      }

/// <summary>
/// Add an IKeyedInserterItem with a specific order.
/// </summary>
/// <param name="order"></param>
/// <param name="item"></param>
      public virtual void Add(T item, int order)
      {
         IList listForOrder = null;
         if (!_orderedList.TryGetValue(order, out listForOrder))
         {
            listForOrder = new List<T>();
            _orderedList.Add(order, listForOrder);
         }
         listForOrder.Add(item);

         T existingItem;
         string key = item.GetKey();
         if (!_sortedByKey.TryGetValue(key, out existingItem))
            _sortedByKey.Add(key, item);

         if (!_itemImplementsIDisposable && item is IDisposable)
            _itemImplementsIDisposable = true;
      }

      public virtual bool Contains(string key)
      {
         T item;
         return _sortedByKey.TryGetValue(key, out item);
      }

/// <summary>
/// Total of unique keys. Mostly intended for testing.
/// </summary>
/// <returns></returns>
      public int CountKeys()
      {
         return _sortedByKey.Count;
      }


      #region IInserter Members

/// <summary>
/// Converts the data into the string that will be inserted.
/// </summary>
/// <remarks>
/// <para>If there have been no items added, it returns the empty string.</para>
/// </remarks>
/// <param name="httpContext"></param>
/// <returns></returns>
      public virtual string GetContent(HttpContextBase httpContext)
      {
         if (_orderedList.Count == 0)
            return String.Empty;

         StringBuilder sb = new StringBuilder();
         PrefixContent(sb, httpContext);
         foreach (var orders in _orderedList)
         {
            foreach (T item in orders.Value)
            {
               ItemContent(item, sb, httpContext);
            }
         }
         PostfixContent(sb, httpContext);
         return sb.ToString();
      }

/// <summary>
/// Used by GetContent to add the initial text to the string builder such as a
/// beginning script tag.
/// </summary>
/// <remarks>
/// <para>This class does not modify the text.</para>
/// </remarks>
/// <param name="sb"></param>
/// <param name="httpContext"></param>
      protected virtual void PrefixContent(StringBuilder sb, HttpContextBase httpContext)
      {
      }

/// <summary>
/// Used by GetContent to add text representing the item into the string builder.
/// </summary>
/// <param name="item"></param>
/// <param name="sb"></param>
/// <param name="httpContext"></param>
      protected abstract void ItemContent(T item, StringBuilder sb, HttpContextBase httpContext);

/// <summary>
/// Used by GetContent to add the final text to the string builder such as a
/// ending script tag.
/// </summary>
/// <remarks>
/// <para>This class does not modify the text.</para>
/// </remarks>
/// <param name="sb"></param>
/// <param name="httpContext"></param>
      protected virtual void PostfixContent(StringBuilder sb, HttpContextBase httpContext)
      {
      }

      #endregion

/// <summary>
/// If needed, calls UpdatePage()
/// </summary>
/// <param name="disposing"></param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (_itemImplementsIDisposable)
            {
               // only need to clear items in _orderedList. _sortedByContent has the same items
               // Additionally _orderdList contains every item while _sortedByContent is only a subset.
               foreach (var item in _orderedList)
               {
                  if (item.Value is IDisposable)
                     ((IDisposable)item.Value).Dispose();
               }
            }
         }
         _orderedList = null;
         _sortedByKey = null;
      }

      #region IDisposable Members

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      #endregion
   }



}
