using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSquared.MvcExtensions.ActionFilters;
using Rhino.Mocks;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using TechTalk.SpecFlow;

namespace NSquared.MvcExtensions.Test
{
    [Binding]
    [Scope(Feature = "使用ActionFilter提供JsonSchema")]
    public class 使用ActionFilter提供JsonSchema步驟
    {
        public ActionExecutingContext Context { get; set; }

        public Exception Exception { get; set; }

        [BeforeScenario]
        public void InitialContext()
        {
            this.Context = new ActionExecutingContext();

            this.Context.HttpContext = MockRepository.GenerateStub<HttpContextBase>();
            this.Context.HttpContext.Expect(i => i.Request)
                                    .Return(MockRepository.GenerateStub<HttpRequestBase>());

            NameValueCollection collection = new NameValueCollection();

            this.Context.HttpContext.Request.Expect(i => i.Headers)
                                            .Return(collection);
        }

        [Given(@"HttpRequest的Header包含 ""(.*)"", 值為 ""(.*)""")]
        public void 假設HttpRequest的Header包含值為(string headerKey, string headerValue)
        {
            this.Context.HttpContext.Request.Headers.Add(headerKey, headerValue);
        }

        [Given(@"指定物件回傳的Type為 ""(.*)""")]
        public void 假設指定物件回傳的Type為(string typeName)
        {
            this.Context.ActionDescriptor = MockRepository.GenerateStub<ActionDescriptor>();

            var type = Type.GetType(typeName);
            ResponseTypeAttribute attribute = new ResponseTypeAttribute(type);

            this.Context.ActionDescriptor.Expect(i => i.GetCustomAttributes(typeof(ResponseTypeAttribute), true))
                                         .Return(new object[] { attribute });

        }

        [Given(@"沒有指定回傳物件的Type")]
        public void 假設沒有指定回傳物件的Type()
        {
            this.Context.ActionDescriptor = MockRepository.GenerateStub<ActionDescriptor>();

            this.Context.ActionDescriptor.Expect(i => i.GetCustomAttributes(typeof(ResponseTypeAttribute), true))
                                         .Return(new object[1]);
        }


        [When(@"執行Action時")]
        public void 當執行Action時()
        {
            try
            {
                EnableJsonSchemaAttribute attribute = new EnableJsonSchemaAttribute();
                attribute.OnActionExecuting(this.Context);
            }
            catch (Exception ex)
            {
                this.Exception = ex;
            }
        }

        [Then(@"回傳內容為""(.*)""")]
        public void 那麼回傳內容為(string content)
        {
            Assert.AreEqual(content, (this.Context.Result as ContentResult).Content);
        }

        [Then(@"沒有回傳內容")]
        public void 那麼沒有回傳內容()
        {
            Assert.IsNull(this.Context.Result);
        }

        [Then(@"拋出錯誤訊息")]
        public void 那麼拋出錯誤訊息()
        {
            Assert.IsNotNull(this.Exception);
        }
    }
}
