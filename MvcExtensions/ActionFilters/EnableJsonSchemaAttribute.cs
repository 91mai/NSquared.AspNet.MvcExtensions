using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NSquared.MvcExtensions.ActionFilters
{
    public class EnableJsonSchemaAttribute : ActionFilterAttribute
    {
        public string HeaderConstraintKeyword { get; set; }

        public string FilePath { get; set; }

        public EnableJsonSchemaAttribute()
        {
            this.HeaderConstraintKeyword = "NSquare-Request-JsonSchema";
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var headerValue = filterContext.HttpContext.Request.Headers[this.HeaderConstraintKeyword];

            //// 有Header才回傳Dummy Data
            if (!string.IsNullOrWhiteSpace(headerValue) &&
                Convert.ToBoolean(headerValue))
            {
                Type returnType = null;

                //// 若有指定Return Type，使用Return Type
                var responseTypeAttribute = filterContext.ActionDescriptor
                                                         .GetCustomAttributes(typeof(ResponseTypeAttribute), true)
                                                         .OfType<ResponseTypeAttribute>()
                                                         .FirstOrDefault();
                if (responseTypeAttribute != null)
                {
                    returnType = responseTypeAttribute.ResponseType;
                }
                else
                {
                    throw new ApplicationException("沒有指定Return Type!");
                }

                //// 產生JsonSchema
                var jsonSchemaGenerator = new JsonSchemaGenerator();
                var jsonSchema = jsonSchemaGenerator.Generate(returnType);
                jsonSchema.Title = returnType.FullName;

                //// 將JsonSchema格式化 (for display)
                var writer = new StringWriter();
                var jsonTextWriter = new JsonTextWriter(writer);
                jsonSchema.WriteTo(jsonTextWriter);                

                //// 回傳Json Schema
                filterContext.Result = new ContentResult
                {
                    Content = writer.ToString(),
                    ContentType = "application/json; charset=utf-8"
                };
            }
        }
    }
}
