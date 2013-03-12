using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;
using System.IO;
using System.Web;

namespace UnitTest
{
   /// <summary>
   /// Summary description for UnitTest1
   /// </summary>
   [TestClass]
   public class TestInsertionsManager
   {

      InsertionsManager CreateInsertionsManager(StringBuilder sb)
      {
         StringWriter writer = new StringWriter(sb);
         HttpContextBase httpContext = new MockHttpContext();
         InserterFactory services = new InserterFactory();
         Assert.IsNotNull(services);
         InsertionsManager manager = new InsertionsManager(writer, httpContext, services);
         Assert.IsNotNull(manager);

         return manager;
      }

      [TestMethod]
      public void Construction()
      {
         StringBuilder sb = new StringBuilder();
         StringWriter writer = new StringWriter(sb);
         HttpContextBase httpContext = new MockHttpContext();
         InserterFactory services = new InserterFactory();
         Assert.IsNotNull(services);
         InsertionsManager manager = new InsertionsManager(writer, httpContext, services);
         Assert.IsNotNull(manager);
         manager.Dispose();
         manager = null;

         manager = new InsertionsManager(writer, httpContext, services);
         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);
         Assert.IsInstanceOfType(contentWriter, typeof(StringWriter));
      }

      [TestMethod]
      public void ReplaceWithNothingCaptured()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual(3, sb.Length);
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokens1()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- replace-with='ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokens2()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!--   replace-with  =  'ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }
      [TestMethod]
      public void ReplaceWithNothingButTokens3()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- replace-with=\"ISomething\"-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithIllegalTokens1()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<span data-xxxxinsertion='ISomething'-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc<span data-xxxxinsertion='ISomething'-->", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithIllegalTokens2()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- replace-with='Some thing'-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc<!-- replace-with='Some thing'-->", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokenAndGroup()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- replace-with='ISomething:GroupName'-->");
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc", sb.ToString());

      }

      [TestMethod]
      public void ReplaceWithNothingButTokenAndGroupIllegal()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);
         contentWriter.Write("abc<!-- replace-with='ISomething:'-->");  // missing group name after : is illegal
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         Assert.AreEqual("abc<!-- replace-with='ISomething:'-->", sb.ToString());

      }


      [TestMethod]
      public void ReplaceScriptFiles1()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInserter inserter = manager.Access<IScriptFilesInserter>();
         Assert.IsNotNull(inserter);
         string url1 = "/Test.js";
         inserter.Add(url1);

         contentWriter.Write("abc<!-- replace-with='IScriptFilesInserter'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         string expected = TestScriptFileInserter.CreateExpected(url1);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFiles2()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInserter inserter = manager.Access<IScriptFilesInserter>();
         Assert.IsNotNull(inserter);
         string url1 = "/Test1.js";
         inserter.Add(url1);
         string url2 = "/Test2.js";
         inserter.Add(url2);

         contentWriter.Write("abc<!-- replace-with='IScriptFilesInserter'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         string expected = TestScriptFileInserter.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithGroups()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter);

         Assert.AreEqual(0, sb.Length);

         IScriptFilesInserter inserterG1 = manager.Access<IScriptFilesInserter>("G1");
         Assert.IsNotNull(inserterG1);
         string url1 = "/Test1.js";
         inserterG1.Add(url1);

         IScriptFilesInserter inserterG2 = manager.Access<IScriptFilesInserter>("G2");
         Assert.IsNotNull(inserterG2);
         string url2 = "/Test2.js";
         inserterG2.Add(url2);

         contentWriter.Write("abc<!-- replace-with='IScriptFilesInserter'-->def<!-- replace-with='IScriptFilesInserter:G1'-->ghi<!-- replace-with='IScriptFilesInserter:g2'-->jkl");   // lowercase g2 to match
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb
         string expectedG1 = TestScriptFileInserter.CreateExpected(url1);
         string expectedG2 = TestScriptFileInserter.CreateExpected(url2);
         Assert.AreEqual("abcdef" + expectedG1 + "ghi" + expectedG2 + "jkl", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithDirectAccess()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInserter>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInserter>().Add(url2);

         contentWriter.Write("abc<!-- replace-with='IScriptFilesInserter'-->def"); 
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb

         string expected = TestScriptFileInserter.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceScriptFilesWithShortName()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInserter>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInserter>().Add(url2);

         contentWriter.Write("abc<!-- replace-with='ScriptFiles'-->def"); // name is missing "I" and "Inserter" in IScriptFilesInserter.
         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb

         string expected = TestScriptFileInserter.CreateExpected(url1, url2);
         Assert.AreEqual("abc" + expected + "def", sb.ToString());

      }

      [TestMethod]
      public void ReplaceAllTypes()
      {
         StringBuilder sb = new StringBuilder();
         InsertionsManager manager = CreateInsertionsManager(sb);

         TextWriter contentWriter = manager.ContentWriter;
         Assert.IsNotNull(contentWriter); 

         Assert.AreEqual(0, sb.Length);

         string url1 = "/Test1.js";
         manager.Access<IScriptFilesInserter>().Add(url1);

         string url2 = "/Test2.js";
         manager.Access<IScriptFilesInserter>().Add(url2);
         contentWriter.Write("scriptfile: <!-- replace-with='IScriptFilesInserter'-->"); 

         string cssurl1 = "/Test1.css";
         manager.Access<IStyleFilesInserter>().Add(cssurl1);
         contentWriter.Write("stylefile: <!-- replace-with='IStyleFilesInserter'-->");

         string name1 = "NAME1";
         string content1 = "CONTENT1";
         manager.Access<IMetaTagsInserter>().Add(name1, content1);
         contentWriter.Write("metatag: <!-- replace-with='IMetaTagsInserter'-->");

         string script1 = "function test1() { }; ";
         manager.Access<IScriptBlocksInserter>("Upper").Add(null, script1);
         contentWriter.Write("upper: <!-- replace-with='IScriptBlocksInserter:Upper'-->");

         string script2 = "function test2() { }; ";
         manager.Access<IScriptBlocksInserter>("Lower").Add(null, script2);
         contentWriter.Write("lower: <!-- replace-with='IScriptBlocksInserter:Lower'-->");


         string name2 = "NAME2";
         string value2 = "VALUE2";
         manager.Access<IHiddenFieldsInserter>().Add(name2, value2);
         contentWriter.Write("hidden: <!-- replace-with='IHiddenFieldsInserter'-->");


         Assert.AreEqual(0, sb.Length);

         manager.UpdatePage();   // transfers to sb

         string expectedScriptFile = TestScriptFileInserter.CreateExpected(url1, url2);
         string expectedStyleFile = TestStyleFileInserter.CreateExpected(cssurl1);
         string expectedMetaTag = TestMetaTagsInserter.CreateExpected(name1, content1);
         string expectedUpperScript = TestScriptBlockInserter.createExpected(script1);
         string expectedLowerScript = TestScriptBlockInserter.createExpected(script2);
         string expectedHiddenField = TestHiddenFieldsInserter.CreateExpected(name2, value2);
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
