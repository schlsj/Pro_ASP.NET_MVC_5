using System;
using System.Diagnostics;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users = "admin")]
        public string Index()
        {
            return "This is the Index action on the Home Controller";
        }

        [GoogleAuth]
        [Authorize(Users = "bob@google.com")]
        public string List()
        {
            return "This is the List action on the Home Controller";
        }

        //[RangeException]
        //[HandleError(ExceptionType = typeof(ArgumentOutOfRangeException))]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException),View = "RangeError")]
        public string RangeTest(int id)
        {
            if (id > 100)
            {
                return $"The id value is: {id}.";
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), id, "");
            }
        }

        [CustomAction]
        public string FilterTest()
        {
            return "This is the filter test action.";
        }

        [ProfileAction]
        [ProfileResult]
        [ProfileAll]
        public string ProfileTest()
        {
            return "This is the action profile test.";
        }

        public string NonAttributeTest()
        {
            return "This is the action nonAttribute test";
        }

        private Stopwatch timer;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer=Stopwatch.StartNew();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write($"<div>Total elapsed time: {timer.Elapsed.TotalSeconds:F6}s.");
        }
    }
}