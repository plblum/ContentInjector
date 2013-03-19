using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestScriptFileInjector : BaseInjector
   {
      public static string CreateExpected(params string[] urls)
      {
         if (urls.Length == 0)
            return String.Empty;
         StringBuilder sb = new StringBuilder();
         foreach (string url in urls)
            sb.AppendLine(String.Format(ScriptFileInjectorItem.DefaultScriptFileTagFormat, url));
         return sb.ToString().TrimEnd('\r', '\n');
      }

      [TestMethod]
      public void TestEmpty()
      {
         ScriptFilesInjector inserter = new ScriptFilesInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected(); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         ScriptFilesInjector inserter = new ScriptFilesInjector();
         string Url = "/Test.js";
         inserter.Add(new ScriptFileInjectorItem(Url));
         Assert.IsTrue(inserter.Contains(Url));

         string expected = CreateExpected(Url); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         ScriptFilesInjector inserter = new ScriptFilesInjector();
         string Url = "/Test.js";
         inserter.Add(new ScriptFileInjectorItem(Url));
         Assert.IsTrue(inserter.Contains(Url));
         inserter.Add(new ScriptFileInjectorItem(Url));   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(Url); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         ScriptFilesInjector inserter = new ScriptFilesInjector();
         string Url1 = "/Test1.js";
         string Url2 = "/Test2.js";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new ScriptFileInjectorItem(Url1));
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new ScriptFileInjectorItem(Url2));
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url1, Url2); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         ScriptFilesInjector inserter = new ScriptFilesInjector();
         string Url1 = "/Test1.js";
         string Url2 = "/Test2.js";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new ScriptFileInjectorItem(Url1), 10);
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new ScriptFileInjectorItem(Url2), 0); // this will be shown before Url1
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url2, Url1); 
         TestInjector(inserter, expected);
      }

/* Low level code requires the System.Web.HttpRuntime object to be setup within a web app to convert "~".
      [TestMethod]
      public void TestVirtualPath()
      {
         ScriptFileInjector inserter = new ScriptFileInjector();
         string Url = "~/Test.js";
         inserter.Add(Url);
         Assert.IsTrue(inserter.Contains(Url));

         string expected = String.Format(ScriptFileInjector.ScriptFileTagPattern, Url) + "\r\n";
         TestInjector(inserter, expected);
      }
*/

   }
}
