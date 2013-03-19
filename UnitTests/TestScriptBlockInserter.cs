using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestScriptBlockInjector : BaseInjector
   {
      [TestMethod]
      public void TestEmpty()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         TestInjector(inserter, "");
      }

      public static string createExpected(params string[] scripts)
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(ScriptBlocksInjector.StartScriptBlockTag);
         foreach (string script in scripts)
         {
            sb.AppendLine(script);
         }

         sb.AppendLine(ScriptBlocksInjector.EndScriptBlockTag);
         return sb.ToString().TrimEnd('\r', '\n');
      }

      [TestMethod]
      public void TestOne()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         string key = "key1";
         string script = "function script1() {};";
         inserter.Add(new ScriptBlockInjectorItem(key, script));
         Assert.IsTrue(inserter.Contains(key));

         string expected = createExpected(script);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestDuplicates()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         string key = "key1";
         string script = "function script1() {};";
         inserter.Add(new ScriptBlockInjectorItem(key, script));
         Assert.IsTrue(inserter.Contains(key));
         string script2 = "function script2() {};";
         inserter.Add(new ScriptBlockInjectorItem(key, script2));   // should not add another as its a duplicate, but it replaces the original script
         Assert.AreEqual(1, inserter.CountKeys());

         string expected = createExpected(script2);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestTwo()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         string key1 = "key1";
         string script1 = "function script1() {};";
         string key2 = "key2";
         string script2 = "function script2() {};";
         Assert.IsFalse(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(new ScriptBlockInjectorItem(key1, script1));
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(new ScriptBlockInjectorItem(key2, script2));
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsTrue(inserter.Contains(key2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = createExpected(script1, script2);
         TestInjector(inserter, expected);
      }

      [TestMethod]
      public void TestOrderedTwo()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         string key1 = "key1";
         string script1 = "function script1() {};";
         string key2 = "key2";
         string script2 = "function script2() {};";
         Assert.IsFalse(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(new ScriptBlockInjectorItem(key1, script1), 10);
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsFalse(inserter.Contains(key2));
         inserter.Add(new ScriptBlockInjectorItem(key2, script2), 0); // this will be shown before key1
         Assert.IsTrue(inserter.Contains(key1));
         Assert.IsTrue(inserter.Contains(key2));
         Assert.AreEqual(2, inserter.CountKeys());

         string expected = createExpected(script2, script1);
         TestInjector(inserter, expected);
      }


   }
}
