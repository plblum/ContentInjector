using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.Mvc;

namespace UnitTest
{
   public class Mock
   {
      // Some ideas from http://blogs.teamb.com/craigstuntz/2010/09/10/38638/
      public static HtmlHelper CreateHtmlHelper()
      {
         var vc = new ViewContext();
         vc.ViewData = new ViewDataDictionary();
         vc.HttpContext = new MockHttpContext();
         var hh = new HtmlHelper(vc, new MockViewDataContainer(vc.ViewData));
         return hh;
      }
      public static HtmlHelper<TModel> CreateHtmlHelper<TModel>(TModel model)
      {
         var vc = new ViewContext();
         vc.ViewData = new ViewDataDictionary<TModel>(model);
         vc.HttpContext = new MockHttpContext();
         var hh = new HtmlHelper<TModel>(vc, new MockViewDataContainer(vc.ViewData));
         return hh;
      }

   }

   public class MockHttpContext : HttpContextBase
   {
      private Dictionary<object, object> _items = new Dictionary<object, object>();
      public override IDictionary Items { get { return _items; } }
   }

   public class MockViewDataContainer : IViewDataContainer
   {
      public MockViewDataContainer(ViewDataDictionary viewData)
      {
         _viewData = viewData;
      }
      private ViewDataDictionary _viewData = new ViewDataDictionary();
      public ViewDataDictionary ViewData { get { return _viewData; } set { _viewData = value; } }
   }
}
