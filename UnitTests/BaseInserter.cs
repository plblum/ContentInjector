using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   public class BaseInjector
   {
      public void TestInjector(IInjector instanceToTest, string expected)
      {
         MockHttpContext context = new MockHttpContext();
         string result = instanceToTest.GetContent(context);
         Assert.AreEqual(expected, result);
      }
   }
}
