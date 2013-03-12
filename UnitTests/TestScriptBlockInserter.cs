using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestScriptBlockInserter : BaseInserter
   {
      [TestMethod]
      public void TestEmpty()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         TestInserter(inserter, "");
      }

      public static string createExpected(params string[] scripts)
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(ScriptBlocksInserter.StartScriptBlockTag);
         foreach (string script in scripts)
         {
            sb.AppendLine(script);
         }

         sb.AppendLine(ScriptBlocksInserter.EndScriptBlockTag);
         return sb.ToString();
      }

      [TestMethod]
      public void TestOne()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         string key = "key1";
         string script = "function script1() {};";
         inserter.Add(key, script);
         Assert.IsTrue(inserter.Contains(key));

         string expected = createExpected(script);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         string key = "key1";
         string script = "function script1() {};";
         inserter.Add(key, script);
         Assert.IsTrue(inserter.Contains(key));
         string script2 = "function script2() {};";
         inserter.Add(key, script2);   // should not add another as its a duplicate, but it replaces the original script
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = createExpected(script2);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         string key1 = "key1";
         string script1 = "function script1() {};";
         string key2 = "key2";
         string script2 = "function script2() {};";
         Assert.IsFalse(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(key1, script1);
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(key2, script2);
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsTrue(inserter.Contains(key2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = createExpected(script1, script2);
         TestInserter(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         string key1 = "key1";
         string script1 = "function script1() {};";
         string key2 = "key2";
         string script2 = "function script2() {};";
         Assert.IsFalse(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(key1, script1, 10);
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(key2, script2, 0); // this will be shown before key1
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsTrue(inserter.Contains(key2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = createExpected(script2, script1);
         TestInserter(inserter, expected);
      }


   }
}
