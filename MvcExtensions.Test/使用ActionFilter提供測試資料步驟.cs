using NSquared.MvcExtensions.ActionFilters;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechTalk.SpecFlow;

namespace NSquared.MvcExtensions.Test
{
    [Binding]
    [Scope(Feature = "使用ActionFilter提供測試資料")]
    public class 使用ActionFilter提供測試資料步驟
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

        [Given(@"測試Json存在路徑 ""(.*)""，內容為 ""(.*)""")]
        public void 假設測試Json存在路徑內容為(string path, string dummyData)
        {
            var realPath = AppDomain.CurrentDomain.BaseDirectory + path.Replace("~", string.Empty);

            this.Context.HttpContext.Expect(i => i.Server)
                                    .Return(MockRepository.GenerateStub<HttpServerUtilityBase>());

            this.Context.HttpContext.Server.Expect(i => i.MapPath(path))
                                           .Return(realPath);

            Path.GetDirectoryName(realPath);

            var directoryName = Path.GetDirectoryName(realPath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(realPath, dummyData);
        }

        [Given(@"沒有測試用的Json")]
        public void 假設沒有測試用的Json()
        {
            this.Context.HttpContext.Expect(i => i.Server)
                                    .Return(MockRepository.GenerateStub<HttpServerUtilityBase>());

            this.Context.HttpContext.Server.Expect(i => i.MapPath(Arg<string>.Is.Anything))
                                           .Return(string.Empty);
        }


        [When(@"執行Controller: ""(.*)"", Action: ""(.*)"" 時")]
        public void 當執行ControllerAction時(string controller, string action)
        {
            this.Context.RequestContext = MockRepository.GenerateStub<RequestContext>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", controller);
            routeData.Values.Add("action", action);

            this.Context.RequestContext.RouteData = routeData;

            try
            {
                EnableDummyAttribute dummyAttribute = new EnableDummyAttribute();
                dummyAttribute.OnActionExecuting(this.Context);
            }
            catch (Exception ex)
            {
                this.Exception = ex;                
            }
        }

        [Then(@"回傳內容為""(.*)""")]
        public void 那麼回傳內容為(string dummyData)
        {
            Assert.AreEqual(dummyData, (this.Context.Result as ContentResult).Content);
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
