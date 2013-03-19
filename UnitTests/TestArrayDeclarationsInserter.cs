using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestScriptBlocksInjectorForArrayDeclarations : BaseInjector
   {
      public static string CreateExpected(params string[] arrays)
      {
         if (arrays.Length == 0)
            return String.Empty;
         StringBuilder sb = new StringBuilder();
         sb.AppendLine(ScriptBlocksInjector.StartScriptBlockTag);
         foreach (string code in arrays)
         {
            sb.AppendLine(code);
         }

         sb.AppendLine(ScriptBlocksInjector.EndScriptBlockTag);
         return sb.ToString().TrimEnd('\r', '\n');
      }

      [TestMethod]
      public void TestEmpty()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         string expected = CreateExpected();
         TestInjector(inserter, expected);

      }

      [TestMethod]
      public void TestOne1()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", 1.5));
         Assert.AreEqual(1, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInjectorItem.CreateExpected("varName1", "1.5"));
         TestInjector(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestOne2()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", "something"));
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", false));
         Assert.AreEqual(1, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInjectorItem.CreateExpected("varName1", "\"something\"", "false"));
         TestInjector(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestTwo1()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", 1.5));
         inserter.Add(new ArrayDeclarationInjectorItem("varName2", "something"));
         Assert.AreEqual(2, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInjectorItem.CreateExpected("varName1", "1.5"), 
            TestArrayDeclarationInjectorItem.CreateExpected("varName2", "\"something\""));
         TestInjector(inserter, expectedGroup);

      }

      [TestMethod]
      public void TestTwo2()
      {
         ScriptBlocksInjector inserter = new ScriptBlocksInjector();
         Assert.AreEqual(0, inserter.CountKeys());
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", 1.5));
         inserter.Add(new ArrayDeclarationInjectorItem("varName1", false));
         inserter.Add(new ArrayDeclarationInjectorItem("varName2", "something"));
         inserter.Add(new ArrayDeclarationInjectorItem("VARNAME2", 400));
         Assert.AreEqual(3, inserter.CountKeys());

         string expectedGroup = CreateExpected(
            TestArrayDeclarationInjectorItem.CreateExpected("varName1", "1.5", "false"), 
            TestArrayDeclarationInjectorItem.CreateExpected("varName2", "\"something\""),
            TestArrayDeclarationInjectorItem.CreateExpected("VARNAME2", "400"));
         TestInjector(inserter, expectedGroup);

      }


   }
}
