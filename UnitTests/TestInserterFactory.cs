using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentInjector;

namespace UnitTest
{
   [TestClass]
   public class TestInjectorFactory
   {
      [TestMethod]
      public void TestFactoryInstance()
      {
         InjectorFactory factory = new InjectorFactory();
         IInjector testInjector = factory.Create(typeof(IScriptFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(ScriptFilesInjector));

         testInjector = factory.Create(typeof(IStyleFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IStyleFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(StyleFilesInjector));

         testInjector = factory.Create(typeof(IMetaTagsInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IMetaTagsInjector));
         Assert.IsInstanceOfType(testInjector, typeof(MetaTagsInjector));

         testInjector = factory.Create(typeof(IScriptBlocksInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptBlocksInjector));
         Assert.IsInstanceOfType(testInjector, typeof(ScriptBlocksInjector));

         testInjector = factory.Create(typeof(IPlaceholderInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IPlaceholderInjector));
         Assert.IsInstanceOfType(testInjector, typeof(PlaceholderInjector));

         testInjector = factory.Create(typeof(IHiddenFieldsInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IHiddenFieldsInjector));
         Assert.IsInstanceOfType(testInjector, typeof(HiddenFieldsInjector));

      }

      [TestMethod]
      public void TestDefaultFactory()
      {
         InjectorFactory factory = InjectorFactory.Default;
         IInjector testInjector = factory.Create(typeof(IScriptFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(ScriptFilesInjector));

         testInjector = factory.Create(typeof(IStyleFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IStyleFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(StyleFilesInjector));

         testInjector = factory.Create(typeof(IMetaTagsInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IMetaTagsInjector));
         Assert.IsInstanceOfType(testInjector, typeof(MetaTagsInjector));

         testInjector = factory.Create(typeof(IScriptBlocksInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptBlocksInjector));
         Assert.IsInstanceOfType(testInjector, typeof(ScriptBlocksInjector));

         testInjector = factory.Create(typeof(IPlaceholderInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IPlaceholderInjector));
         Assert.IsInstanceOfType(testInjector, typeof(PlaceholderInjector));


         testInjector = factory.Create(typeof(IHiddenFieldsInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IHiddenFieldsInjector));
         Assert.IsInstanceOfType(testInjector, typeof(HiddenFieldsInjector));
      }

      [TestMethod]
      public void TestReplace()
      {
         InjectorFactory factory = new InjectorFactory();

         IInjector testInjector = factory.Create(typeof(IScriptFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(ScriptFilesInjector));

         factory.Register(typeof(IScriptFilesInjector), typeof(CustomScriptFileInjector));
         testInjector = factory.Create(typeof(IScriptFilesInjector));
         Assert.IsNotNull(testInjector);
         Assert.IsInstanceOfType(testInjector, typeof(IScriptFilesInjector));
         Assert.IsInstanceOfType(testInjector, typeof(CustomScriptFileInjector));
      }



      private class CustomScriptFileInjector : ScriptFilesInjector
      {
      }
   }
}
