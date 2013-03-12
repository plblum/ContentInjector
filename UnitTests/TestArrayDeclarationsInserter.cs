using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestScriptBlocksInserterForArrayDeclarations : BaseInserter
   {
      public static string CreateExpected(params string[] arrays)
      {
         if (arrays.Length == 0)
            return String.Empty;
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(ScriptBlocksInserter.StartScriptBlockTag);
         foreach (string code in arrays)
         {
            sb.AppendLine(code);
         }

         sb.AppendLine(ScriptBlocksInserter.EndScriptBlockTag);
         return sb.ToString();
      }

      [TestMethod]
      public void TestEmpty()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected();
         TestInserter(inserter, expected);

      }

      [TestMethod]
      public void TestOne1()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.ArrayDeclaration("varName1", 1.5);
         Assert.AreEqual(1, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInserterItem.CreateExpected("varName1", "1.5"));
         TestInserter(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestOne2()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.ArrayDeclaration("varName1", "something");
         inserter.ArrayDeclaration("varName1", false);
         Assert.AreEqual(1, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInserterItem.CreateExpected("varName1", "\"something\"", "false"));
         TestInserter(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestTwo1()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.ArrayDeclaration("varName1", 1.5);
         inserter.ArrayDeclaration("varName2", "something");
         Assert.AreEqual(2, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInserterItem.CreateExpected("varName1", "1.5"), 
            TestArrayDeclarationInserterItem.CreateExpected("varName2", "\"something\""));
         TestInserter(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestTwo2()
      {
         ScriptBlocksInserter inserter = new ScriptBlocksInserter();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.ArrayDeclaration("varName1", 1.5);
         inserter.ArrayDeclaration("varName1", false);
         inserter.ArrayDeclaration("varName2", "something");
         inserter.ArrayDeclaration("VARNAME2", 400);
         Assert.AreEqual(3, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInserterItem.CreateExpected("varName1", "1.5", "false"), 
            TestArrayDeclarationInserterItem.CreateExpected("varName2", "\"something\""),
            TestArrayDeclarationInserterItem.CreateExpected("VARNAME2", "400"));
         TestInserter(inserter, expectedGroup);

      }


   }
}
