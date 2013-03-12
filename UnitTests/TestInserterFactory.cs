using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertionsManagement;

namespace UnitTest
{
   [TestClass]
   public class TestInserterFactory
   {
      [TestMethod]
      public void TestFactoryInstance()
      {
         InserterFactory factory = new InserterFactory();
         IInserter testInserter = factory.Create(typeof(IScriptFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(ScriptFilesInserter));

         testInserter = factory.Create(typeof(IStyleFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IStyleFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(StyleFilesInserter));

         testInserter = factory.Create(typeof(IMetaTagsInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IMetaTagsInserter));
         Assert.IsInstanceOfType(testInserter, typeof(MetaTagsInserter));

         testInserter = factory.Create(typeof(IScriptBlocksInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptBlocksInserter));
         Assert.IsInstanceOfType(testInserter, typeof(ScriptBlocksInserter));

         testInserter = factory.Create(typeof(IPlaceholderInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IPlaceholderInserter));
         Assert.IsInstanceOfType(testInserter, typeof(PlaceholderInserter));

         testInserter = factory.Create(typeof(IHiddenFieldsInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IHiddenFieldsInserter));
         Assert.IsInstanceOfType(testInserter, typeof(HiddenFieldsInserter));

      }

      [TestMethod]
      public void TestDefaultFactory()
      {
         InserterFactory factory = InserterFactory.Default;
         IInserter testInserter = factory.Create(typeof(IScriptFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(ScriptFilesInserter));

         testInserter = factory.Create(typeof(IStyleFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IStyleFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(StyleFilesInserter));

         testInserter = factory.Create(typeof(IMetaTagsInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IMetaTagsInserter));
         Assert.IsInstanceOfType(testInserter, typeof(MetaTagsInserter));

         testInserter = factory.Create(typeof(IScriptBlocksInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptBlocksInserter));
         Assert.IsInstanceOfType(testInserter, typeof(ScriptBlocksInserter));

         testInserter = factory.Create(typeof(IPlaceholderInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IPlaceholderInserter));
         Assert.IsInstanceOfType(testInserter, typeof(PlaceholderInserter));


         testInserter = factory.Create(typeof(IHiddenFieldsInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IHiddenFieldsInserter));
         Assert.IsInstanceOfType(testInserter, typeof(HiddenFieldsInserter));
      }

      [TestMethod]
      public void TestReplace()
      {
         InserterFactory factory = new InserterFactory();

         IInserter testInserter = factory.Create(typeof(IScriptFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(ScriptFilesInserter));

         factory.Register(typeof(IScriptFilesInserter), typeof(CustomScriptFileInserter));
         testInserter = factory.Create(typeof(IScriptFilesInserter));
         Assert.IsNotNull(testInserter);
         Assert.IsInstanceOfType(testInserter, typeof(IScriptFilesInserter));
         Assert.IsInstanceOfType(testInserter, typeof(CustomScriptFileInserter));
      }



      private class CustomScriptFileInserter : ScriptFilesInserter
      {
      }
   }
}
