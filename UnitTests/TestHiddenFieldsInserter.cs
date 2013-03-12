using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestHiddenFieldsInserter : BaseInserter
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
            string value = pairs[i + 1];
            sb.AppendLine(String.Format(HiddenFieldsInserter.HiddenFieldPattern, name, value));
         }
         return sb.ToString();
      }

      [TestMethod]
      public void TestEmpty()
      {
         HiddenFieldsInserter inserter = new HiddenFieldsInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected();
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOne()
      {
         HiddenFieldsInserter inserter = new HiddenFieldsInserter();
         string name = "NAME1";
         string value = "VALUE1";
         inserter.Add(name, value);
         Assert.IsTrue(inserter.Contains(name));

         string expected = CreateExpected(name, value);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         HiddenFieldsInserter inserter = new HiddenFieldsInserter();
         string name = "NAME1";
         string value = "VALUE1";
         inserter.Add(name, value);
         Assert.IsTrue(inserter.Contains(name));
         string value2 = "VALUE2";
         inserter.Add(name, value2);   // should not add another as its a duplicate
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = CreateExpected(name, value);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         HiddenFieldsInserter inserter = new HiddenFieldsInserter();
         string name1 = "NAME1";
         string value1 = "VALUE1";
         string name2 = "NAME2";
         string value2 = "VALUE2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name1, value1);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name2, value2);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name1, value1, name2, value2);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         HiddenFieldsInserter inserter = new HiddenFieldsInserter();
         string name1 = "NAME1";
         string value1 = "VALUE1";
         string name2 = "NAME2";
         string value2 = "VALUE2";
         Assert.IsFalse(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name1, value1, 10);
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsFalse(inserter.Contains(name2));
         inserter.Add(name2, value2, 0); // this will be shown before name1
         Assert.IsTrue(inserter.Contains(name1));
         Assert.IsTrue(inserter.Contains(name2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = CreateExpected(name2, value2, name1, value1);
         TestInserter(inserter, expected);
      }


   }
}
