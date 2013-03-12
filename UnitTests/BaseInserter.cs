using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   public class BaseInserter
   {
      public void TestInserter(IInserter instanceToTest, string expected)
      {
         MockHttpContext context = new MockHttpContext();
         string result = instanceToTest.GetContent(context);
         Assert.AreEqual(expected, result);
      }
   }
}
