using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestStyleFileInserter : BaseInserter
   {
      public static string CreateExpected(params string[] urls)
      {
         if (urls.Length == 0)
            return String.Empty;
         StringBuilder sb = new StringBuilder();
         foreach (string url in urls)
            sb.AppendLine(String.Format(StyleFilesInserter.StyleFileTagPattern, url));
         return sb.ToString();
      }


      [TestMethod]
      public void TestEmpty()
      {
         StyleFilesInserter inserter = new StyleFilesInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected(); 
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         StyleFilesInserter inserter = new StyleFilesInserter();
         string Url = "/Test.css";
         inserter.Add(Url);
         Assert.IsTrue(inserter.Contains(Url));

         string expected = CreateExpected(Url); 
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         StyleFilesInserter inserter = new StyleFilesInserter();
         string Url = "/Test.css";
         inserter.Add(Url);
         Assert.IsTrue(inserter.Contains(Url));
         inserter.Add(Url);   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(Url); 
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         StyleFilesInserter inserter = new StyleFilesInserter();
         string Url1 = "/Test1.css";
         string Url2 = "/Test2.css";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(Url1);
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(Url2);
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url1, Url2); 
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         StyleFilesInserter inserter = new StyleFilesInserter();
         string Url1 = "/Test1.css";
         string Url2 = "/Test2.css";
         Assert.IsFalse(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(Url1, 10);
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsFalse(inserter.Contains(Url2));
         inserter.Add(Url2, 0); // this will be shown before Url1
         Assert.IsTrue(inserter.Contains(Url1));
         Assert.IsTrue(inserter.Contains(Url2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(Url2, Url1); 
         TestInserter(inserter, expected);
      }


   }
}
