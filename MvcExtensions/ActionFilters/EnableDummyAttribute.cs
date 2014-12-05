using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NSquare.MvcExtensions.ActionFilters
{
    public class EnableDummyAttribute : ActionFilterAttribute
    {
        public string HeaderConstraintKeyword { get; set; }

        public string FilePath { get; set; }

        public EnableDummyAttribute()
        {
            this.HeaderConstraintKeyword = "NSquare-Request-Dummy";
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var headerValue = filterContext.HttpContext.Request.Headers[this.HeaderConstraintKeyword];

            //// 有Header才回傳Dummy Data
            if (!string.IsNullOrWhiteSpace(headerValue) &&
                Convert.ToBoolean(headerValue))
            {
                var filePath = this.FilePath;

                //// 產生Dummy Data路徑
                if (string.IsNullOrWhiteSpace(this.FilePath))
                {
                    string actionName = filterContext.RequestContext.RouteData.GetRequiredString("action");
                    string controllerName = filterContext.RequestContext.RouteData.GetRequiredString("controller");

                    filePath = string.Format("~/Dummy/{0}/{1}.json", controllerName, actionName);
                }

                //// 判斷真實檔案路徑
                if (filePath.Contains("~"))
                {
                    filePath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
                }

                //// 檢查是否有Dummy Data
                if (!File.Exists(filePath))
                {
                    throw new ApplicationException("Dummy Data不存在");
                }

                //// 將字串轉為json
                var fileContent = File.ReadAllText(filePath);
                var parsedJson = JsonConvert.DeserializeObject(fileContent);

                //// 回傳Dummy Data                
                filterContext.Result = new ContentResult
                {
                    Content = fileContent,
                    ContentType = "application/json; charset=utf-8"
                };
            }
        }
    }
}
