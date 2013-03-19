using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestStyleFileInjector : BaseInjector
   {
      public static string CreateExpected(params string[] urls)
      {
         if (urls.Length == 0)
            return String.Empty;
         StringBuilder sb = new StringBuilder();
         foreach (string url in urls)
            sb.AppendLine(String.Format(StyleFileInjectorItem.DefaultStyleFileTagFormat, url));
         return sb.ToString().TrimEnd('\r', '\n');
      }


      [TestMethod]
      public void TestEmpty()
      {
         StyleFilesInjector inserter = new StyleFilesInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected(); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         StyleFilesInjector inserter = new StyleFilesInjector();
         string Url = "/Test.css";
         inserter.Add(new StyleFileInjectorItem(Url));
         Assert.IsTrue(inserter.Contains(Url));

         string expected = CreateExpected(Url); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         StyleFilesInjector inserter = new StyleFilesInjector();
         string Url = "/Test.css";
         inserter.Add(new StyleFileInjectorItem(Url));
         Assert.IsTrue(inserter.Contains(Url));
         inserter.Add(new StyleFileInjectorItem(Url));   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(Url); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         StyleFilesInjector inserter = new StyleFilesInjector();
         string Url1 = "/Test1.css";
         string Url2 = "/Test2.css";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new StyleFileInjectorItem(Url1));
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new StyleFileInjectorItem(Url2));
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url1, Url2); 
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         StyleFilesInjector inserter = new StyleFilesInjector();
         string Url1 = "/Test1.css";
         string Url2 = "/Test2.css";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new StyleFileInjectorItem(Url1), 10);
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(new StyleFileInjectorItem(Url2), 0); // this will be shown before Url1
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url2, Url1); 
         TestInjector(inserter, expected);
      }


   }
}
