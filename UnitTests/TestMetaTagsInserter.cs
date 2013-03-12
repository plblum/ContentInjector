using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestMetaTagsInserter : BaseInserter
   {

      public static string CreateExpected(params string[] pairs)
      {
         if (pairs.Length == 0)
            return String.Empty;
         if (pairs.Length % 2 == 1)
            Assert.Fail("Must pass even number of parameters.");
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < pairs.Length; i = i + 2)
         {
            string name = pairs[i];
            string content = pairs[i + 1];
            sb.AppendLine(String.Format(MetaTagsInserter.MetaTagPattern, name, content, "name"));
         }
         return sb.ToString();
      }

      [TestMethod]
      public void TestEmpty()
      {
         MetaTagsInserter inserter = new MetaTagsInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected();
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         MetaTagsInserter inserter = new MetaTagsInserter();
         string name = "NAME1";
         string content = "CONTENT1";
         inserter.Add(name, content);
         Assert.IsTrue(inserter.Contains(name));

         string expected = CreateExpected(name, content);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         MetaTagsInserter inserter = new MetaTagsInserter();
         string name = "NAME1";
         string content = "CONTENT1";
         inserter.Add(name, content);
         Assert.IsTrue(inserter.Contains(name));
         string content2 = "CONTENT2";
         inserter.Add(name, content2);   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(name, content);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         MetaTagsInserter inserter = new MetaTagsInserter();
         string name1 = "NAME1";
         string content1 = "CONTENT1";
         string name2 = "NAME2";
         string content2 = "CONTENT2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name1, content1);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name2, content2);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name1, content1, name2, content2);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         MetaTagsInserter inserter = new MetaTagsInserter();
         string name1 = "NAME1";
         string content1 = "CONTENT1";
         string name2 = "NAME2";
         string content2 = "CONTENT2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name1, content1, 10);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name2, content2, 0); // this will be shown before name1
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name2, content2, name1, content1);
         TestInserter(inserter, expected);
      }


   }
}
