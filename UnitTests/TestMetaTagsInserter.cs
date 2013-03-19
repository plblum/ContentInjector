using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestMetaTagsInjector : BaseInjector
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
            sb.AppendLine(String.Format(MetaTagInjectorItem.DefaultMetaTagFormat, name, content, "name"));
         }
         return sb.ToString().TrimEnd('\r', '\n');
      }

      [TestMethod]
      public void TestEmpty()
      {
         MetaTagsInjector inserter = new MetaTagsInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected();
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         MetaTagsInjector inserter = new MetaTagsInjector();
         string name = "NAME1";
         string content = "CONTENT1";
         inserter.Add(new MetaTagInjectorItem(name, content));
         Assert.IsTrue(inserter.Contains(name));

         string expected = CreateExpected(name, content);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         MetaTagsInjector inserter = new MetaTagsInjector();
         string name = "NAME1";
         string content = "CONTENT1";
         inserter.Add(new MetaTagInjectorItem(name, content));
         Assert.IsTrue(inserter.Contains(name));
         string content2 = "CONTENT2";
         inserter.Add(new MetaTagInjectorItem(name, content2));   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(name, content);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         MetaTagsInjector inserter = new MetaTagsInjector();
         string name1 = "NAME1";
         string content1 = "CONTENT1";
         string name2 = "NAME2";
         string content2 = "CONTENT2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(new MetaTagInjectorItem(name1, content1));
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(new MetaTagInjectorItem(name2, content2));
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name1, content1, name2, content2);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         MetaTagsInjector inserter = new MetaTagsInjector();
         string name1 = "NAME1";
         string content1 = "CONTENT1";
         string name2 = "NAME2";
         string content2 = "CONTENT2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(new MetaTagInjectorItem(name1, content1), 10);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(new MetaTagInjectorItem(name2, content2), 0); // this will be shown before name1
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name2, content2, name1, content1);
         TestInjector(inserter, expected);
      }


   }
}
