using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestArrayDeclarationInjectorItem
   {

      public static string CreateExpected(string variableName, params string[] elements)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("var ");
         sb.Append(variableName);
         sb.Append(" = [");
         bool first = true;
         for (int i = 0; i < elements.Length; i++)
         {
            if (!first)
               sb.Append(", ");
            first = false;
            sb.Append(elements[i]);
         }
         sb.AppendLine("];");
         return sb.ToString();
      }


      [TestMethod]
      public void TestEmpty()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);

         string expected = CreateExpected(varName);
         Assert.AreEqual(expected, item.GetContent(null));

      }

      [TestMethod]
      public void TestInts()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);
         item.Add(1);
         item.Add((short)2);
         item.Add((long)3);

         string expected = CreateExpected(varName, "1", "2", "3");
         Assert.AreEqual(expected, item.GetContent(null));
      }

      [TestMethod]
      public void TestDecimals()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);
         item.Add(1.5);
         item.Add((Single)2.5);
         item.Add(3.5M);

         string expected = CreateExpected(varName, "1.5", "2.5", "3.5");
         Assert.AreEqual(expected, item.GetContent(null));
      }

      [TestMethod]
      public void TestStrings()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);
         item.Add("abc");
         item.Add("ab\"c");
         item.Add("abc<hi>");
         item.Add("abc<hi>", false);

         string expected = CreateExpected(varName, "\"abc\"", "\"ab&quot;c\"", "\"abc&lt;hi&gt;\"", "\"abc<hi>\"");
         Assert.AreEqual(expected, item.GetContent(null));
      }

      [TestMethod]
      public void TestBoolean()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);
         item.Add(true);
         item.Add(false);

         string expected = CreateExpected(varName, "true", "false");
         Assert.AreEqual(expected, item.GetContent(null));
      }

      [TestMethod]
      public void TestCode()
      {
         string varName = "varName1";
         ArrayDeclarationInjectorItem item = new ArrayDeclarationInjectorItem(varName);
         item.Add(1.5);
         item.AddCode("alert('hi')");
         item.Add(3.5M);

         string expected = CreateExpected(varName, "1.5", "alert('hi')", "3.5");
         Assert.AreEqual(expected, item.GetContent(null));
      }

   }
}
