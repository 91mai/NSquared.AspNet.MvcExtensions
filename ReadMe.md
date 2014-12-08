# NSquared.AspNet.Extensions [![](https://api.travis-ci.org/91mai/NSquared.AspNet.Extensions.png?branch=master)](https://travis-ci.org/91mai/NSquared.AspNet.Extensions)

Provides Asp.Net Mvc and Asp.Net WebApi Extensions to easily add json schema provide or dummy data support.

*  NSquared.MvcExtensions
*  NSquared.WebApiExtensions

## How to use?

1. To install NSquared.MvcExtensions, run the following command in the Package Manager Console

		Install-Package NSquared.MvcExtensions

1. Add attribute to your Asp.Net Mvc Controller

		[EnableJsonSchema]
    	[EnableDummy]
    	public class SampleController : Controller
    	{
			[ResponseType(typeof(myData))]
			public ActionResult Get(){
			}
		}

1. For dummy data, you should add data which you want to return by path **~/Dummy/ControllerName/ActionName.json**, and add header **NSquared-Request-Dummy=true** in your request

		﹂Controllers
		﹂Dummy
			﹂Sample
				﹂Get.json

1. For json schema, specify responseType in your action first, and then just need add header **NSquared-Request-JsonSchema** in your request.

## LICENSE

(The MIT License)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.