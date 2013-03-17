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
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ContentInjector
{
/// <summary>
/// Hosts Injector classes that are collecting contents from the user.
/// It directs the user to those Injectors so they can add contents.
/// It outputs the content into Injection Points in the page.
/// </summary>
/// <remarks>
/// <para>Attach to a ViewEngine Page before processing contents.</para>
/// <para>Exposed through the static "Injector" class. 
/// After the page's contents are generated, call PagePostProcessor() to 
/// replace placeholders with the collected contents.</para>
/// <para>When using Razor WebPages, typically subclass the 
/// System.Web.Mvc.RazorView class and override its RenderView()
/// method, creating the ContentManager before calling base.RenderView
/// and calling the PagePostProcessor() method after base.RenderView returns.</para>
/// <para>You will need to create a StringWriter that will collect the content
/// and pass it along to base.RenderView. The results are passed to PagePostProcessor()
/// which returns a string that is written to the original writer.</para>
/// <para>This class is intended for single-thread operations.</para>
/// </remarks>
   public class ContentManager : IDisposable
   {

/// <summary>
/// Constructor
/// </summary>
/// <param name="originalWriter">The TextWriter originally intended to capture the markup.</param>
/// <param name="httpContext">The HttpContext for this request</param>
/// <param name="injecterFactory">Provides instances of IInjector classes. If null or not supplied,
/// the global default is used (InjectorFactory.Default).</param>
      public ContentManager(TextWriter originalWriter, HttpContextBase httpContext, InjectorFactory injecterFactory = null)
      {
         if (originalWriter == null)
            throw new ArgumentNullException("originalWriter");
         _originalWriter = originalWriter;

         if (httpContext == null)
            throw new ArgumentNullException("httpContext");
         _httpContext = httpContext;

         if (injecterFactory == null)
            injecterFactory = InjectorFactory.Default;
         _injectorFactory = injecterFactory;
     }

/// <summary>
/// Provides instances of IInjector classes.
/// </summary>
      protected InjectorFactory _injectorFactory;

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
/// Instances of the Injector classes that are in use. One instance per unique
/// interface type + group name.
/// </summary>
      protected List<InjectorContainer> _injecters = new List<InjectorContainer>();

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
/// Determines if and how errors are reported when there was no Injection Point
/// for content that was registered.
/// </summary>
/// <remarks>
/// <para>Set this in Application_Start or appSettings of web.config using key="CIReportErrorsMode"
/// and value= "None", "Trace", or "Exception"</para>
/// </remarks>
/// <value>
/// <para>If not assigned or setup in appSettings, it defaults to ReportErrorsMode.Exception.</para>
/// </value>
      public static ReportErrorsMode ReportErrorsMode
      {
         get
         {
            if (!_reportErrorsMode.HasValue)
            {
               // check appSettings. If not there, use the default
               string result = WebConfigurationManager.AppSettings["CIReportErrorsMode"];
               switch (result)
               {
                  case "None":
                     _reportErrorsMode = ReportErrorsMode.None;
                     break;
                  case "Trace":
                     _reportErrorsMode = ReportErrorsMode.Trace;
                     break;
                  default:
                     _reportErrorsMode = ReportErrorsMode.Exception;
                     break;
               }
            }
            return _reportErrorsMode.Value;
         }
         set { _reportErrorsMode = value; }
      }
      private static ReportErrorsMode? _reportErrorsMode;

/// <summary>
/// Primary entry point to retrieve the class that implements IInjector
/// to add to it. Uses the default group.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
      public T Access<T>()
         where T : IInjector
      {
         return Access<T>(String.Empty);
      }

/// <summary>
/// Primary entry point to retrieve the class that implements IInjector
/// to add to it.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="group">The subgroup or "" if no group is used. Typically
/// this parameter is omitted.</param>
/// <returns></returns>
      public T Access<T>(string group)
         where T: IInjector
      {
         if (group == null)
            group = "";

         Type interfaceType = typeof(T);
         InjectorContainer injectorContainer = _injecters.FirstOrDefault<InjectorContainer>(
            c => c.InterfaceType == interfaceType && 
                 String.Compare(c.Group, group, StringComparison.OrdinalIgnoreCase) == 0);
         if (injectorContainer == null)
         {
            IInjector injecter = _injectorFactory.Create(interfaceType);
            if (injecter == null)
               throw new ArgumentException(String.Format("The InjectorFactory lacks a registered entry for the {0} interface type.", typeof(T).FullName));
            injectorContainer = new InjectorContainer(interfaceType, group, injecter);
            _injecters.Add(injectorContainer);
         }
         return (T) injectorContainer.Injector;
      }


/// <summary>
/// Adds an Insertion Point, which is a marker in the content left for the post processor
/// to replace.
/// Typically used with the razor syntax like this: @Inserter.InjectionPoint("name", "group")
/// </summary>
/// <remarks>
/// <para>When called, no content from Injectors is written. Instead, a notation 
/// is inserted into the HTML. That notation is recognized by the PagePostProcessor() method
/// where the content replaces the Insertion Point.</para>
/// </remarks>
/// <param name="injectorName"></param>
/// <param name="groupName"></param>
/// <returns></returns>
      public virtual MvcHtmlString InjectionPoint(string injectorName, string groupName = "")
      {
         injectorName = NormalizeInjectorName(injectorName);
         if (String.IsNullOrEmpty(groupName))
            return new MvcHtmlString(String.Format(InjectionPointTemplate, injectorName));
         else
            return new MvcHtmlString(String.Format(InjectionPointTemplateWithGroup, injectorName, groupName));
      }

/// <summary>
/// Template for creating an insertion point without a group name.
/// </summary>
/// <remarks>
/// <para>Designed to be replaceable in Application_Start. If you replace it,
/// also update TextSearchRE, a regular expression to capture these Insertion Points.</para>
/// <para>Requires the {0} token which is replaced by the InsertionPoint name.</para>
/// </remarks>
      public static string InjectionPointTemplate = "<!-- Injection-Point=\"{0}\" -->";
/// <summary>
/// Template for creating an insertion point with a group name.
/// </summary>
/// <remarks>
/// <para>Designed to be replaceable in Application_Start. If you replace it,
/// also update TextSearchRE, a regular expression to capture these Insertion Points.</para>
/// <para>Requires the {0} token which is replaced by the InsertionPoint name.
/// and {1} which is replaced by the group name.</para>
/// </remarks>
      public static string InjectionPointTemplateWithGroup = "<!-- Injection-Point=\"{0}:{1}\" -->";

/// <summary>
/// Call after the ancestor prepares the page to replace the placeholders.
/// Those placeholders that have no matching injecter that was used will
/// be replaced by an empty string.
/// </summary>
      public virtual void PagePostProcessor()
      {
         if (_contentWriter == null)
            throw new InvalidOperationException("Must pass the ContentCapture property as the Writer parameter of the View's Render method.");
         if (hasUpdated)
            return;


         string content = _content.ToString();
         if (content.Length == 0)
            return;

         _injectorsRemaining = new List<InjectorContainer>(_injecters); // this will have items removed by InsertionMatchEvaluator()
         string newContent = UpdateContent(content);

         _originalWriter.Write(newContent);

         hasUpdated = true;
      }

      private bool hasUpdated;// = false;

/// <summary>
/// PagePostProcessor() captures each Injector that was not used to replace an injecter here
/// so a later call to ReportErrors() can use these to report errors.
/// </summary>
      protected List<InjectorContainer> _injectorsRemaining;

/// <summary>
/// Creates the regular expression used by UpdatePanel to find markup
/// that will be replaced.
/// </summary>
/// <remarks>
/// <para>Normally you will call PagePostProcessor which calls this with data from the _contentWriter.
/// However, this is exposed to let you replace content from any source where you have retrieved
/// the data into a string that is passed here.</para>
/// <para>Subclass if you want to change the pattern.</para>
/// </remarks>
/// <returns></returns>
      public virtual string UpdateContent(string content)
      {
         return TextSearchRE.Replace(content, InjectionMatchEvaluator);
      }

/// <summary>
/// Looks for a comment with specific content like this: &lt;!-- Injection-Point="interface name" --&gt; such as:
/// &lt;!-- Injection-Point="ILowerScriptBlocksInjector" --&gt;
/// or 
/// &lt;!-- Injection-Point="LowerScriptBlocks" --&gt;
/// as you can omit the lead "I" and "Injector" elements in the interface name.
/// It supports groups by using &lt;!-- Injection-Point="interface name:group name" --&gt; like this:
/// &lt;!-- Injection-Point="LowerScriptBlocks:MyGroup" --&gt;
/// </summary>
/// <remarks>
/// <para>Public global so you can replace it with alternatives in Application_Start,
/// such as using a different string from "Injection-Point".</para>
/// </remarks>
      public static Regex TextSearchRE = new Regex(@"\<\!\-\-\s+Injection-Point\s*=\s*[" + "\"" + @"'](?<name>\w+)(\:(?<group>\w+))?[" + "\"" + @"']\s*\-\-\>", RegexOptions.Compiled);

/// <summary>
/// Used by PagePostProcessor to return the content that replaces a token
/// found by the regular expression.
/// </summary>
/// <param name="match"></param>
/// <returns></returns>
      protected virtual string InjectionMatchEvaluator(Match match)
      {
         string interfaceName = NormalizeInjectorName(match.Groups["name"].Value);

         string groupName = match.Groups["group"].Success ? match.Groups["group"].Value : String.Empty;
         foreach (var injecterContainer in _injecters)
         {
            if (String.Compare(injecterContainer.InterfaceType.Name, interfaceName, StringComparison.Ordinal) == 0) // exact match
               if (String.Compare(injecterContainer.Group, groupName, StringComparison.OrdinalIgnoreCase) == 0)
               {
                  _injectorsRemaining.Remove(injecterContainer);
                  return injecterContainer.Injector.GetContent(_httpContext);
               }
         }
         return String.Empty;
      }

/// <summary>
/// Takes a name for an injector. Convert it to an interface name if needed
/// by adding a lead "I" and trailing "Injector".
/// </summary>
/// <param name="injectorName"></param>
/// <returns></returns>
      protected virtual string NormalizeInjectorName(string injectorName)
      {
         if (!injectorName.StartsWith("I"))
         { // user can omit the lead I. If they do, they can also omit the trailing Injector.
            injectorName = "I" + injectorName;
            if (!injectorName.EndsWith("Injector", StringComparison.OrdinalIgnoreCase))
               injectorName += "Injector";
         }
         return injectorName;
      }


/// <summary>
/// Call after PagePostProcessor to deliver a list of missing Injection Points,
/// where content added to the ContentManager but that code never made it to the page.
/// Determines how to report the error from the ContentManager.ReportsErrorMode global property.
/// </summary>
      public virtual void ReportErrors(HttpContextBase httpContext)
      {
         ReportErrors(httpContext, ContentManager.ReportErrorsMode);
      }

/// <summary>
/// Call after PagePostProcessor to deliver a list of missing Injection Points,
/// where content added to the ContentManager but that code never made it to the page.
/// </summary>
      public virtual void ReportErrors(HttpContextBase httpContext, ReportErrorsMode reportErrorsMode)
      {
         if ((reportErrorsMode == ContentInjector.ReportErrorsMode.None) || (_injectorsRemaining == null) || (_injectorsRemaining.Count == 0))
            return;

         StringBuilder errorMessage = new StringBuilder();

         bool first = true;
         errorMessage.Append("Content was added to the ContentManager but not output because the page lacks an Injection Point. The following Injection Points are needed: ");
         foreach (var item in _injectorsRemaining)
         {
            if (!first)
               errorMessage.Append("; ");
            string interfaceName = item.InterfaceType.Name;
            if (interfaceName.StartsWith("I"))
               interfaceName = interfaceName.Remove(0, 1);
            if (interfaceName.EndsWith("Injector", StringComparison.OrdinalIgnoreCase))
               interfaceName = interfaceName.Remove(interfaceName.Length - "Injector".Length);

            errorMessage.Append("\"");
            errorMessage.Append(interfaceName);
            if (!String.IsNullOrEmpty(item.Group))
            {
               errorMessage.Append(":");
               errorMessage.Append(item.Group);
            }
            errorMessage.Append("\"");
            first = false;
         }
         switch (reportErrorsMode)
         {
            case ContentInjector.ReportErrorsMode.Trace:
               httpContext.Trace.Warn("ContentManager", errorMessage.ToString());
               break;
            case ContentInjector.ReportErrorsMode.Exception:
               throw new InvalidOperationException(errorMessage.ToString());
         }
      }

/// <summary>
/// If needed, calls PagePostProcessor()
/// </summary>
/// <param name="disposing"></param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
// This may be inappropriate as an exception raised may not need this to happen
            if (_contentWriter != null)
               PagePostProcessor();


          _content = null;
        }
      // writers may use resources.
         if (_contentWriter != null)
            _contentWriter.Dispose();
         _contentWriter = null;

         _originalWriter = null; // ContentManager does not own this instance, so it will not Dispose it
         _injectorFactory = null; // ContentManager does not own this instance, so it will not Dispose it
      }

      #region IDisposable Members

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      #endregion

/// <summary>
/// An element in the injectors collection.
/// There must be unique interface type + group pair for each element in the list.
/// </summary>
      protected class InjectorContainer
      {
         public InjectorContainer(Type interfaceType, string group, IInjector injecter)
         {
            InterfaceType = interfaceType;
            Group = group ?? String.Empty;
            Injector = injecter;
         }

/// <summary>
/// Identifies the IInjector interface type associated with the Injector.
/// </summary>
         public Type InterfaceType { get; protected set; }

/// <summary>
/// There can be multiple injecters of the same interface type so long as they
/// have a different value here. It lets you offer the same type
/// of data in multiple places on the page. The placeholder
/// tag uses this syntax to indicate a group:
/// Injection-Point="type:group". When no group is specified, this is "".
/// Group names are matched case insensitively.
/// </summary>
         public string Group { get; protected set; }

         public IInjector Injector { get; protected set; }
      }
   }

/// <summary>
/// Associated with the ReportErrorsMode property on ContentManager.
/// </summary>
   public enum ReportErrorsMode
   {
/// <summary>
/// Do not output errors
/// </summary>
      None,
/// <summary>
/// Write to the trace log
/// </summary>
      Trace,
/// <summary>
/// Throw an exception.
/// </summary>
      Exception
   }


}
