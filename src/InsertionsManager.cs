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
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace InsertionsManagement
{
/// <summary>
/// Attach to a ViewEngine Page before processing contents.
/// Expose it as a property like "Insertions" and the contents 
/// can use it's Access method to interact with an Inserter class.
/// After the page's contents are generated, call UpdatePage() to 
/// replace placeholders with the collected contents.
/// </summary>
/// <remarks>
/// <para>When using Razor WebPages, typically subclass the 
/// System.Web.Mvc.RazorView class and override its RenderView()
/// method, creating the Insertion Manager before calling base.RenderView
/// and calling the UpdatePage() method after base.RenderView returns.</para>
/// <para>You will need to create a StringWriter that will collect the content
/// and pass it along to base.RenderView. The results are passed to UpdatePage()
/// which returns a string that is written to the original writer.</para>
/// <para>This class is intended for single-thread operations.</para>
/// </remarks>
   public class InsertionsManager : IDisposable
   {

/// <summary>
/// Constructor
/// </summary>
/// <param name="originalWriter">The TextWriter originally intended to capture the markup.</param>
/// <param name="httpContext">The HttpContext for this request</param>
/// <param name="inserterFactory">Provides instances of IInserter classes. If null or not supplied,
/// the global default is used (InserterFactory.Default).</param>
      public InsertionsManager(TextWriter originalWriter, HttpContextBase httpContext, InserterFactory inserterFactory = null)
      {
         if (originalWriter == null)
            throw new ArgumentNullException("originalWriter");
         _originalWriter = originalWriter;

         if (httpContext == null)
            throw new ArgumentNullException("httpContext");
         _httpContext = httpContext;

         if (inserterFactory == null)
            inserterFactory = InserterFactory.Default;
         _inserterFactory = inserterFactory;
     }

/// <summary>
/// Provides instances of IInserter classes.
/// </summary>
      protected InserterFactory _inserterFactory;

/// <summary>
/// The TextWriter originally intended to capture the markup.
/// It is set by the constructor.
/// </summary>
      protected TextWriter _originalWriter;

/// <summary>
/// The HttpContext for this request.
/// </summary>
      protected HttpContextBase _httpContext;

/// <summary>
/// Instances of the Inserter classes that are in use. One instance per unique
/// interface type + group name.
/// </summary>
      protected List<InserterContainer> _inserters = new List<InserterContainer>();

/// <summary>
/// Holds the text captured from the content of the page.
/// It will eventually be transferred to the originalWriter
/// </summary>
      public TextWriter ContentWriter
      {
         get
         {
            if (_contentWriter == null)
               _contentWriter = CreateContentWriter();
            return _contentWriter;
         }
      }
      protected TextWriter _contentWriter;

/// <summary>
/// Used by the ContentCapture property to create its initial version.
/// </summary>
/// <returns>This class uses a StringWriter.</returns>
      protected virtual TextWriter CreateContentWriter()
      {
         _content = new StringBuilder(1000);

         return new StringWriter(_content);
      }
      protected StringBuilder _content;

/// <summary>
/// Primary entry point to retrieve the class that implements IInserter
/// to add to it. Uses the default group.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
      public T Access<T>()
         where T : IInserter
      {
         return Access<T>(String.Empty);
      }

/// <summary>
/// Primary entry point to retrieve the class that implements IInserter
/// to add to it.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="group">The subgroup or "" if no group is used. Typically
/// this parameter is omitted.</param>
/// <returns></returns>
      public T Access<T>(string group)
         where T: IInserter
      {
         if (group == null)
            group = "";

         Type interfaceType = typeof(T);
         InserterContainer inserterContainer = _inserters.FirstOrDefault<InserterContainer>(
            c => c.InterfaceType == interfaceType && 
                 String.Compare(c.Group, group, StringComparison.OrdinalIgnoreCase) == 0);
         if (inserterContainer == null)
         {
            IInserter inserter = _inserterFactory.Create(interfaceType);
            if (inserter == null)
               throw new ArgumentException(String.Format("The InserterFactory lacks a registered entry for the {0} interface type.", typeof(T).FullName));
            inserterContainer = new InserterContainer(interfaceType, group, inserter);
            _inserters.Add(inserterContainer);
         }
         return (T) inserterContainer.Inserter;
      }

/// <summary>
/// Call after the ancestor prepares the page to replace the placeholders.
/// Those placeholders that have no matching inserter that was used will
/// be replaced by an empty string.
/// </summary>
      public virtual void UpdatePage()
      {
         if (_contentWriter == null)
            throw new InvalidOperationException("Must pass the ContentCapture property as the Writer parameter of the View's Render method.");
         if (hasUpdated)
            return;

         string content = _content.ToString();
         if (content.Length == 0)
            return;

         string newContent = UpdateContent(content);

         _originalWriter.Write(newContent);

         hasUpdated = true;
      }

      private bool hasUpdated = false;


/// <summary>
/// Creates the regular expression used by UpdatePanel to find markup
/// that will be replaced.
/// </summary>
/// <remarks>
/// <para>Normally you will call UpdatePage which calls this with data from the _contentWriter.
/// However, this is exposed to let you replace content from any source where you have retrieved
/// the data into a string that is passed here.</para>
/// <para>Subclass if you want to change the pattern.</para>
/// </remarks>
/// <returns></returns>
      public virtual string UpdateContent(string content)
      {
         return TextSearchRE.Replace(content, InsertionMatchEvaluator);
      }

/// <summary>
/// Looks for a comment with specific content like this: &lt;!-- replace-with="interface name" --&gt; such as:
/// &lt;!-- replace-with="ILowerScriptBlocksInserter" --&gt;
/// or 
/// &lt;!-- replace-with="LowerScriptBlocks" --&gt;
/// as you can omit the lead "I" and "Inserter" elements in the interface name.
/// It supports groups by using &lt;!-- replace-with="interface name:group name" --&gt; like this:
/// &lt;!-- replace-with="LowerScriptBlocks:MyGroup" --&gt;
/// </summary>
/// <remarks>
/// <para>Public global so you can replace it with alternatives in Application_Start,
/// such as using a different string from "data-insertion".</para>
/// </remarks>
      public static Regex TextSearchRE = new Regex(@"\<\!\-\-\s+replace-with\s*=\s*[" + "\"" + @"'](?<name>\w+)(\:(?<group>\w+))?[" + "\"" + @"']\s*\-\-\>", RegexOptions.Compiled);

/// <summary>
/// Used by UpdatePage to return the content that replaces a token
/// found by the regular expression.
/// </summary>
/// <param name="match"></param>
/// <returns></returns>
      protected virtual string InsertionMatchEvaluator(Match match)
      {
         string interfaceName = match.Groups["name"].Value;
         if (!interfaceName.StartsWith("I"))
         { // user can omit the lead I. If they do, they can also omit the trailing Inserter.
            interfaceName = "I" + interfaceName;
            if (!interfaceName.EndsWith("Inserter", StringComparison.OrdinalIgnoreCase))
               interfaceName += "Inserter";
         }

         string groupName = match.Groups["group"].Success ? match.Groups["group"].Value : String.Empty;
         foreach (var inserterContainer in _inserters)
         {
            if (String.Compare(inserterContainer.InterfaceType.Name, interfaceName, StringComparison.Ordinal) == 0) // exact match
               if (String.Compare(inserterContainer.Group, groupName, StringComparison.OrdinalIgnoreCase) == 0)
                  return inserterContainer.Inserter.GetContent(_httpContext);
         }
         return String.Empty;
      }


/// <summary>
/// If needed, calls UpdatePage()
/// </summary>
/// <param name="disposing"></param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
// This may be inappropriate as an exception raised may not need this to happen
            if (_contentWriter != null)
               UpdatePage();


          _content = null;
        }
      // writers may use resources.
         if (_contentWriter != null)
            _contentWriter.Dispose();
         _contentWriter = null;

         _originalWriter = null; // InsertionsManager does not own this instance, so it will not Dispose it
         _inserterFactory = null; // InsertionsManager does not own this instance, so it will not Dispose it
      }

      #region IDisposable Members

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      #endregion

/// <summary>
/// An element in the inserters collection.
/// There must be unique interface type + group pair for each element in the list.
/// </summary>
      protected class InserterContainer
      {
         public InserterContainer(Type interfaceType, string group, IInserter inserter)
         {
            InterfaceType = interfaceType;
            Group = group ?? String.Empty;
            Inserter = inserter;
         }

/// <summary>
/// Identifies the IInserter interface type associated with the Inserter.
/// </summary>
         public Type InterfaceType { get; protected set; }

/// <summary>
/// There can be multiple inserters of the same interface type so long as they
/// have a different value here. It lets you offer the same type
/// of data in multiple places on the page. The placeholder
/// tag uses this syntax to indicate a group:
/// data-insertion="type:group". When no group is specified, this is "".
/// Group names are matched case insensitively.
/// </summary>
         public string Group { get; protected set; }

         public IInserter Inserter { get; protected set; }
      }
   }


}
