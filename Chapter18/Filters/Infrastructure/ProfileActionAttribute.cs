using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileActionAttribute : FilterAttribute,IActionFilter
    {
        private Stopwatch timer;
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer=Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.Write($"<div>Action method elapsed time: {timer.Elapsed.TotalSeconds:F6}s</div>");
            }
        }
    }
}