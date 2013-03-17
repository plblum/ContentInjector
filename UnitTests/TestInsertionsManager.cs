using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;
using System.IO;
using System.Web;

namespace UnitTest
{
   /// <summary>
   /// Summary description for UnitTest1
   /// </summary>
   [TestClass]
   public class TestContentManager
   {

      ContentManager CreateContentManager(StringBuilder sb)
      {
         StringWriter writer = new StringWriter(sb);
         HttpContextBase httpContext = new MockHttpContext();
         InjectorFactory services = new InjectorFactory();
         Assert.IsNotNull(services);
         ContentManager manager = new ContentManager(writer, httpContext, services);
         Assert.IsNotNull(manager);

         return manager;
      }

      [TestMethod]
      public void Construction()
      {
         StringBuilder sb = new StringBuilder();
         StringWriter writer = new StringWriter(sb);
         HttpContextBase httpContext = new MockHttpContext();
         InjectorFactory services = new InjectorFactory();
         Assert.IsNotNull(services);
         ContentManager manager = new ContentManager(writer, httpContext, services);
         Assert.IsNotNull(manager);
         manager.Dispose();
         manager = null;

         manager = new ContentManager(writer, httpContext, services);
         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);
         Assert.IsInstanceOfType(contentWriter, typeof(StringWriter));
      }

      [TestMethod]
      public void ReplaceWithNothingCaptured()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual(3, sb.Length);
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokens1()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- Injection-Point='ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokens2()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!--   Injection-Point  =  'ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }
      [TestMethod]
      public void ReplaceWithNothingButTokens3()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- Injection-Point=\"ISomething\"-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithIllegalTokens1()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<span data-xxxxinsertion='ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc<span data-xxxxinsertion='ISomething'-->", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithIllegalTokens2()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- Injection-Point='Some thing'-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc<!-- Injection-Point='Some thing'-->", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokenAndGroup()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- Injection-Point='ISomething:GroupName'-->");
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokenAndGroupIllegal()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- Injection-Point='ISomething:'-->");  // missing group name after : is illegal
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         Assert.AreEqual("abc<!-- Injection-Point='ISomething:'-->", sb.ToString());

      }


      [TestMethod]
      public void ReplaceScriptFiles1()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInjector inserter = manager.Access<IScriptFilesInjector>();
         Assert.IsNotNull(inserter);
         string url1 = "/Test.js";
         inserter.Add(url1);

         contentWriter.Write("abc<!-- Injection-Point='IScriptFilesInjector'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         string expected = TestScriptFileInjector.CreateExpected(url1);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFiles2()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInjector inserter = manager.Access<IScriptFilesInjector>();
         Assert.IsNotNull(inserter);
         string url1 = "/Test1.js";
         inserter.Add(url1);
         string url2 = "/Test2.js";
         inserter.Add(url2);

         contentWriter.Write("abc<!-- Injection-Point='IScriptFilesInjector'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         string expected = TestScriptFileInjector.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithGroups()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInjector inserterG1 = manager.Access<IScriptFilesInjector>("G1");
         Assert.IsNotNull(inserterG1);
         string url1 = "/Test1.js";
         inserterG1.Add(url1);

         IScriptFilesInjector inserterG2 = manager.Access<IScriptFilesInjector>("G2");
         Assert.IsNotNull(inserterG2);
         string url2 = "/Test2.js";
         inserterG2.Add(url2);

         contentWriter.Write("abc<!-- Injection-Point='IScriptFilesInjector'-->def<!-- Injection-Point='IScriptFilesInjector:G1'-->ghi<!-- Injection-Point='IScriptFilesInjector:g2'-->jkl");   // lowercase g2 to match
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb
         string expectedG1 = TestScriptFileInjector.CreateExpected(url1);
         string expectedG2 = TestScriptFileInjector.CreateExpected(url2);
         Assert.AreEqual("abcdef" + expectedG1 + "ghi" + expectedG2 + "jkl", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithDirectAccess()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInjector>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInjector>().Add(url2);

         contentWriter.Write("abc<!-- Injection-Point='IScriptFilesInjector'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb

         string expected = TestScriptFileInjector.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithShortName()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInjector>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInjector>().Add(url2);

         contentWriter.Write("abc<!-- Injection-Point='ScriptFiles'-->def"); // name is missing "I" and "Injector" in IScriptFilesInjector.
         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb

         string expected = TestScriptFileInjector.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceAllTypes()
      {
         StringBuilder sb = new StringBuilder();
         ContentManager manager = CreateContentManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInjector>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInjector>().Add(url2);
         contentWriter.Write("scriptfile: <!-- Injection-Point='IScriptFilesInjector'-->"); 

         string cssurl1 = "/Test1.css";
         manager.Access<IStyleFilesInjector>().Add(cssurl1);
         contentWriter.Write("stylefile: <!-- Injection-Point='IStyleFilesInjector'-->");

         string name1 = "NAME1";
         string content1 = "CONTENT1";
         manager.Access<IMetaTagsInjector>().Add(name1, content1);
         contentWriter.Write("metatag: <!-- Injection-Point='IMetaTagsInjector'-->");

         string script1 = "function test1() { }; ";
         manager.Access<IScriptBlocksInjector>("Upper").Add(null, script1);
         contentWriter.Write("upper: <!-- Injection-Point='IScriptBlocksInjector:Upper'-->");

         string script2 = "function test2() { }; ";
         manager.Access<IScriptBlocksInjector>("Lower").Add(null, script2);
         contentWriter.Write("lower: <!-- Injection-Point='IScriptBlocksInjector:Lower'-->");


         string name2 = "NAME2";
         string value2 = "VALUE2";
         manager.Access<IHiddenFieldsInjector>().Add(name2, value2);
         contentWriter.Write("hidden: <!-- Injection-Point='IHiddenFieldsInjector'-->");


         Assert.AreEqual(0, sb.Length);

         manager.PagePostProcessor();   // transfers to sb

         string expectedScriptFile = TestScriptFileInjector.CreateExpected(url1, url2);
         string expectedStyleFile = TestStyleFileInjector.CreateExpected(cssurl1);
         string expectedMetaTag = TestMetaTagsInjector.CreateExpected(name1, content1);
         string expectedUpperScript = TestScriptBlockInjector.createExpected(script1);
         string expectedLowerScript = TestScriptBlockInjector.createExpected(script2);
         string expectedHiddenField = TestHiddenFieldsInjector.CreateExpected(name2, value2);
         Assert.AreEqual(
            "scriptfile: " + expectedScriptFile + 
            "stylefile: " + expectedStyleFile +
            "metatag: " + expectedMetaTag +
            "upper: " + expectedUpperScript +
            "lower: " + expectedLowerScript + 
            "hidden: " + expectedHiddenField, 
            sb.ToString());

      }


   }
}
