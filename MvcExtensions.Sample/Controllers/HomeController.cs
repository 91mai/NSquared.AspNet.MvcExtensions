using NSquared.MvcExtensions.ActionFilters;
using NSquared.MvcExtensions.Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NSquared.MvcExtensions.Sample.Controllers
{
    [EnableDummy]
    [EnableJsonSchema]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ResponseType(typeof(TestData))]
        public ActionResult Dummy()
        {
            throw new NotImplementedException();
        }
    }
}